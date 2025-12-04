using AutoMapper;
using BusinessLogic.DTO.BookingDTOs;
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
    private readonly UserManager<IdentityUser> _userManager; // <--- YENİ

    public BookingService(
        IRepository<Booking> bookingRepository,
        IHouseRepository houseRepository,
        IMapper mapper,
        UserManager<IdentityUser> userManager) // <--- Constructor-a əlavə et
    {
        _bookingRepository = bookingRepository;
        _houseRepository = houseRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<ICollection<BookingGetDTO>> GetAllAsync()
    {
        var list = await _bookingRepository.GetAllAsync("House");
        var dtos = _mapper.Map<ICollection<BookingGetDTO>>(list);

        // Hər bir rezervasiya üçün emaili tapırıq
        foreach (var item in dtos)
        {
            if (!string.IsNullOrEmpty(item.UserId))
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                item.UserEmail = user?.Email;
            }
        }

        return dtos;
    }

    // Digər metodlar olduğu kimi qalır...
    // Sadəcə GetAllActiveAsync, GetByHouseIdAsync kimi metodlarda da eyni email doldurma məntiqini tətbiq edə bilərsən.
    // Məsələn:
    
    public async Task<ICollection<BookingGetDTO>> GetAllActiveAsync()
    {
        var list = await _bookingRepository
            .GetAllByCondition(x => !x.IsDeleted, "House")
            .ToListAsync();

        var dtos = _mapper.Map<ICollection<BookingGetDTO>>(list);
        
        foreach (var item in dtos)
        {
            if (!string.IsNullOrEmpty(item.UserId))
            {
                var user = await _userManager.FindByIdAsync(item.UserId);
                item.UserEmail = user?.Email;
            }
        }
        
        return dtos;
    }

    // ... (Digər metodlar eyni qalır: CreateAsync, UpdateAsync və s.) ...
    // KODUN QALAN HİSSƏSİNİ DƏYİŞMƏYƏ EHTİYAC YOXDUR (Aşağıdakı hissələri olduğu kimi saxla)
    
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

        var hasOverlap = await _bookingRepository
            .GetAllByCondition(b =>
                    !b.IsDeleted &&
                    b.HouseId == dto.HouseId &&
                    dto.StartDate < b.EndDate &&
                    dto.EndDate > b.StartDate)
            .AnyAsync();

        if (hasOverlap) throw new InvalidOperationException("Seçilən tarix aralığında bu ev artıq rezerv edilib.");

        var entity = _mapper.Map<Booking>(dto);

        if (!Enum.IsDefined(typeof(BookingStatus), entity.Status))
            entity.Status = BookingStatus.Pending;

        await _bookingRepository.AddAsync(entity);
        await _bookingRepository.SaveChangesAsync();

        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, BookingPutDTO dto)
    {
        var entity = await _bookingRepository.GetByIdAsync(id);
        if (entity is null || entity.IsDeleted) return;

        if (dto.StartDate >= dto.EndDate) throw new ArgumentException("Başlama tarixi bitmə tarixindən kiçik olmalıdır.");

        var hasOverlap = await _bookingRepository
            .GetAllByCondition(b =>
                    !b.IsDeleted &&
                    b.HouseId == entity.HouseId &&
                    b.Id != entity.Id &&
                    dto.StartDate < b.EndDate &&
                    dto.EndDate > b.StartDate)
            .AnyAsync();

        if (hasOverlap) throw new InvalidOperationException("Seçilən tarix aralığı başqa rezervasiya ilə toqquşur.");

        entity.StartDate = dto.StartDate;
        entity.EndDate = dto.EndDate;
        entity.Status = dto.Status;

        _bookingRepository.Update(entity);
        await _bookingRepository.SaveChangesAsync();
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
}