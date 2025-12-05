using AutoMapper;
using BusinessLogic.DTO.BookingDTOs;
using BusinessLogic.ExternalService.Abstractions;
using BusinessLogic.Service.Abstractions;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity; // <--- LAZIMDIR
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Service.Implementations;

public class BookingService : IBookingService
{
    private readonly IRepository<Booking> _bookingRepository;
    private readonly IHouseRepository _houseRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly UserManager<IdentityUser> _userManager; // <--- YENİ

    public BookingService(
        IRepository<Booking> bookingRepository,
        IHouseRepository houseRepository,
        IMapper mapper,
        UserManager<IdentityUser> userManager, IEmailService emailService) // <--- Constructor-a əlavə et
    {
        _bookingRepository = bookingRepository;
        _houseRepository = houseRepository;
        _mapper = mapper;
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task<ICollection<BookingGetDTO>> GetAllAsync()
    {
        var list = await _bookingRepository.GetAllAsync("House");
        return await MapBookingsWithUserInfo(list);
    }

    public async Task<ICollection<BookingGetDTO>> GetAllActiveAsync()
    {
        var list = await _bookingRepository
            .GetAllByCondition(x => !x.IsDeleted, "House")
            .ToListAsync();
        return await MapBookingsWithUserInfo(list);
    }
    
    public async Task<ICollection<BookingGetDTO>> GetByHouseIdAsync(Guid houseId)
    {
        var list = await _bookingRepository
            .GetAllByCondition(x => !x.IsDeleted && x.HouseId == houseId, "House")
            .OrderBy(x => x.StartDate)
            .ToListAsync();
        return _mapper.Map<ICollection<BookingGetDTO>>(list);
    }

    public async Task<ICollection<BookingGetDTO>> GetByUserIdAsync(string userId)
    {
        var list = await _bookingRepository
            .GetAllByCondition(x => !x.IsDeleted && x.UserId == userId, "House")
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        return _mapper.Map<ICollection<BookingGetDTO>>(list);
    }

    public async Task<BookingGetDTO?> GetByIdAsync(Guid id)
    {
        var entity = await _bookingRepository.GetByIdAsync(id, "House");
        if (entity is null) return null;
        var dto = _mapper.Map<BookingGetDTO>(entity);
        
        if (!string.IsNullOrEmpty(dto.UserId))
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            dto.UserEmail = user?.Email;
        }
        
        return dto;
    }

    public async Task<Guid> CreateAsync(BookingPostDTO dto)
    {
        var house = await _houseRepository.GetByIdAsync(dto.HouseId);
        if (house is null || house.IsDeleted) throw new InvalidOperationException("Ev tapılmadı.");

        if (dto.StartDate >= dto.EndDate) throw new ArgumentException("Başlama tarixi bitmə tarixindən kiçik olmalıdır.");

        // Tarix çakışması yoxlanışı
        var hasOverlap = await _bookingRepository
            .GetAllByCondition(b =>
                    !b.IsDeleted &&
                    b.HouseId == dto.HouseId &&
                    b.Status != BookingStatus.Canceled && // Ləğv olunanları sayma
                    dto.StartDate < b.EndDate &&
                    dto.EndDate > b.StartDate)
            .AnyAsync();

        if (hasOverlap) throw new InvalidOperationException("Seçilən tarix aralığında bu ev artıq rezerv edilib.");

        var entity = _mapper.Map<Booking>(dto);

        if (!Enum.IsDefined(typeof(BookingStatus), entity.Status))
            entity.Status = BookingStatus.Pending;

        await _bookingRepository.AddAsync(entity);
        await _bookingRepository.SaveChangesAsync();

        // --- E-POÇT BİLDİRİŞİ ---
        if (!string.IsNullOrEmpty(dto.UserId))
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                var body = $@"
                    <div style='font-family: Arial, sans-serif; color: #333;'>
                        <h2 style='color: #FF5E14;'>Təşəkkürlər, {user.UserName}!</h2>
                        <p>Sizin <strong>{house.Title}</strong> evi üçün rezervasiya sorğunuz qəbul edildi.</p>
                        <hr/>
                        <p><strong>Giriş:</strong> {dto.StartDate:dd.MM.yyyy}</p>
                        <p><strong>Çıxış:</strong> {dto.EndDate:dd.MM.yyyy}</p>
                        <p><strong>Status:</strong> <span style='color: orange;'>Gözləyir</span></p>
                        <hr/>
                        <p>Admin təsdiqindən sonra sizə əlavə məlumat veriləcək.</p>
                        <p><em>Güvən Turizm Komandası</em></p>
                    </div>";

                // Asinxron göndər (gözlətmədən)
                _ = _emailService.SendEmailAsync(user.Email, "Yeni Rezervasiya - Güvən Turizm", body);
            }
        }

        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, BookingPutDTO dto)
    {
        var entity = await _bookingRepository.GetByIdAsync(id, "House");
        if (entity is null || entity.IsDeleted) return;

        if (dto.StartDate >= dto.EndDate) throw new ArgumentException("Başlama tarixi bitmə tarixindən kiçik olmalıdır.");

        // Tarix çakışması (özü istisna olmaqla)
        var hasOverlap = await _bookingRepository
            .GetAllByCondition(b =>
                    !b.IsDeleted &&
                    b.HouseId == entity.HouseId &&
                    b.Id != entity.Id &&
                    b.Status != BookingStatus.Canceled &&
                    dto.StartDate < b.EndDate &&
                    dto.EndDate > b.StartDate)
            .AnyAsync();

        if (hasOverlap) throw new InvalidOperationException("Seçilən tarix aralığı başqa rezervasiya ilə toqquşur.");

        // Status dəyişikliyini yadda saxla
        bool statusChanged = entity.Status != dto.Status;

        entity.StartDate = dto.StartDate;
        entity.EndDate = dto.EndDate;
        entity.Status = dto.Status;

        _bookingRepository.Update(entity);
        await _bookingRepository.SaveChangesAsync();

        // --- STATUS DƏYİŞƏNDƏ E-POÇT ---
        if (statusChanged && !string.IsNullOrEmpty(entity.UserId))
        {
            var user = await _userManager.FindByIdAsync(entity.UserId);
            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                string statusText = dto.Status == BookingStatus.Confirmed ? "Təsdiqləndi ✅" : "Ləğv edildi ❌";
                string color = dto.Status == BookingStatus.Confirmed ? "green" : "red";
                string message = dto.Status == BookingStatus.Confirmed 
                    ? "Sizi səbirsizliklə gözləyirik!" 
                    : "Təəssüf ki, rezervasiyanız ləğv edildi.";

                var body = $@"
                    <div style='font-family: Arial, sans-serif; color: #333;'>
                        <h2 style='color: #FF5E14;'>Hörmətli {user.UserName},</h2>
                        <p>Sizin <strong>{entity.House?.Title}</strong> üçün olan rezervasiya statusunuz dəyişdi.</p>
                        <div style='padding: 15px; background-color: #f9f9f9; border-radius: 5px; margin: 20px 0;'>
                            <p style='font-size: 18px; margin: 0;'>Yeni Status: <strong style='color:{color}'>{statusText}</strong></p>
                        </div>
                        <p>{message}</p>
                        <hr/>
                        <p><em>Güvən Turizm Komandası</em></p>
                    </div>";

                _ = _emailService.SendEmailAsync(user.Email, $"Rezervasiya {statusText}", body);
            }
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _bookingRepository.GetByIdAsync(id);
        if (entity is null) return;
        _bookingRepository.Delete(entity);
        await _bookingRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var entity = await _bookingRepository.GetByIdAsync(id);
        if (entity is null || entity.IsDeleted) return;
        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;
        _bookingRepository.Update(entity);
        await _bookingRepository.SaveChangesAsync();
    }

    public async Task RestoreAsync(Guid id)
    {
        var entity = await _bookingRepository.GetByIdAsync(id);
        if (entity is null || !entity.IsDeleted) return;
        entity.IsDeleted = false;
        entity.DeletedAt = null;
        _bookingRepository.Update(entity);
        await _bookingRepository.SaveChangesAsync();
    }
    
    private async Task<ICollection<BookingGetDTO>> MapBookingsWithUserInfo(IEnumerable<Booking> bookings)
    {
        var dtos = _mapper.Map<List<BookingGetDTO>>(bookings);

        foreach (var dto in dtos)
        {
            if (!string.IsNullOrEmpty(dto.UserId))
            {
                var user = await _userManager.FindByIdAsync(dto.UserId);
                if (user != null)
                {
                    dto.UserName = user.UserName;
                    dto.UserEmail = user.Email;
                    dto.UserPhoneNumber = user.PhoneNumber; // <--- Telefonu ötürürük
                }
            }
        }
        return dtos;
    }
}