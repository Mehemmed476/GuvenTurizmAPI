using AutoMapper;
using BusinessLogic.DTO.TourDTOs;
using BusinessLogic.ExternalService.Abstractions;
using BusinessLogic.Service.Abstractions;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Service.Implementations;

public class TourService : ITourService
{
    private readonly ITourRepository _tourRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public TourService(ITourRepository tourRepository, IFileService fileService, IMapper mapper)
    {
        _tourRepository = tourRepository;
        _fileService = fileService;
        _mapper = mapper;
    }

    // 1. TÜM TURLARI GETİR (Admin Paneli İçin)
    public async Task<ICollection<TourGetDTO>> GetAllToursAsync()
    {
        var tours = await _tourRepository.GetAllAsync(
            "TourFiles", 
            "TourPackages", 
            "TourPackages.Inclusions"
        );
        return _mapper.Map<ICollection<TourGetDTO>>(tours);
    }

    // 2. AKTİF TURLARI SAYFALI GETİR (Site Arayüzü İçin)
    public async Task<ICollection<TourGetDTO>> GetAllActiveToursAsync(int page = 1, int size = 9)
    {
        // HouseService'deki mantığın aynısı: IsDeleted olmayanları filtrele
        var query = _tourRepository.GetAllByCondition(
            x => !x.IsDeleted && x.IsActive, // Hem silinmemiş hem de aktif işaretli olanlar
            "TourFiles",
            "TourPackages",
            "TourPackages.Inclusions"
        );

        // Pagination (Sayfalama)
        var tours = await query
            .OrderByDescending(x => x.CreatedAt) // En yeniler en üstte
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return _mapper.Map<ICollection<TourGetDTO>>(tours);
    }

    // 3. LOKASYONA GÖRE GETİR (Filtreleme)
    public async Task<ICollection<TourGetDTO>> GetToursByLocationAsync(string location)
    {
        var tours = await _tourRepository.GetAllByCondition(
            x => !x.IsDeleted && x.IsActive && x.Location.Contains(location), // İsme göre arama
            "TourFiles",
            "TourPackages",
            "TourPackages.Inclusions"
        ).ToListAsync();

        return _mapper.Map<ICollection<TourGetDTO>>(tours);
    }

    // 4. ID'YE GÖRE GETİR (Detay Sayfası)
    public async Task<TourGetDTO> GetTourByIdAsync(Guid id)
    {
        var tour = await _tourRepository.GetByIdAsync(id,
            "TourFiles",
            "TourPackages",
            "TourPackages.Inclusions"
        );

        return _mapper.Map<TourGetDTO>(tour);
    }

    // 5. OLUŞTURMA (Create)
    public async Task CreateTourAsync(TourPostDTO dto)
    {
        var tour = _mapper.Map<Tour>(dto);

        // Resimler
        if (dto.Files != null && dto.Files.Count > 0)
        {
            tour.TourFiles = new List<TourFile>();
            foreach (var file in dto.Files)
            {
                var path = await _fileService.SaveAsync(file, "tours/gallery");
                tour.TourFiles.Add(new TourFile 
                { 
                    Path = path, 
                    FileName = file.FileName, 
                    ContentType = file.ContentType,
                    IsMain = tour.TourFiles.Count == 0 // İlk resim kapak resmi olsun
                });
            }
        }

        // Paketler
        if (dto.Packages != null)
        {
            tour.TourPackages = new List<TourPackage>();
            foreach (var pDTO in dto.Packages)
            {
                var pkg = new TourPackage 
                { 
                    PackageName = pDTO.PackageName, 
                    Price = pDTO.Price, 
                    DiscountPrice = pDTO.DiscountPrice,
                    Inclusions = new List<TourPackageInclusion>()
                };

                if (pDTO.Inclusions != null)
                    foreach(var inc in pDTO.Inclusions) 
                        pkg.Inclusions.Add(new TourPackageInclusion { Description = inc });

                tour.TourPackages.Add(pkg);
            }
        }

        await _tourRepository.AddAsync(tour);
        await _tourRepository.SaveChangesAsync();
    }

    // 6. GÜNCELLEME (Update)
    public async Task UpdateTourAsync(Guid id, TourPutDTO dto)
    {
        var tour = await _tourRepository.GetByIdAsync(id, "TourFiles", "TourPackages", "TourPackages.Inclusions");
        if (tour == null) return;

        // Temel Bilgiler
        tour.Title = dto.Title;
        tour.Description = dto.Description;
        tour.Location = dto.Location;
        tour.DurationDay = dto.DurationDay;
        tour.DurationNight = dto.DurationNight;
        tour.StartDate = dto.StartDate;

        // Resim Silme
        if (dto.ImageIdsToDelete != null && dto.ImageIdsToDelete.Count > 0)
        {
            var toDelete = tour.TourFiles.Where(x => dto.ImageIdsToDelete.Contains(x.Id)).ToList();
            foreach (var img in toDelete)
            {
                await _fileService.DeleteAsync(img.Path);
                tour.TourFiles.Remove(img);
            }
        }

        // Yeni Resim Ekleme
        if (dto.NewImages != null && dto.NewImages.Count > 0)
        {
            if (tour.TourFiles == null) tour.TourFiles = new List<TourFile>();
            foreach (var file in dto.NewImages)
            {
                var path = await _fileService.SaveAsync(file, "tours/gallery");
                tour.TourFiles.Add(new TourFile { Path = path, FileName = file.FileName, ContentType = file.ContentType });
            }
        }

        // Paket Güncelleme (Sil ve Yeniden Ekle Mantığı)
        if (dto.Packages != null)
        {
            tour.TourPackages.Clear(); // Eskileri temizle
            foreach (var pDTO in dto.Packages)
            {
                var pkg = new TourPackage
                {
                    PackageName = pDTO.PackageName,
                    Price = pDTO.Price,
                    DiscountPrice = pDTO.DiscountPrice,
                    Inclusions = new List<TourPackageInclusion>()
                };
                
                if (pDTO.Inclusions != null)
                    foreach (var inc in pDTO.Inclusions)
                        pkg.Inclusions.Add(new TourPackageInclusion { Description = inc });

                tour.TourPackages.Add(pkg);
            }
        }

        _tourRepository.Update(tour);
        await _tourRepository.SaveChangesAsync();
    }

    public async Task DeleteTourAsync(Guid id)
    {
        var tour = await _tourRepository.GetByIdAsync(id, "TourFiles");
        if (tour == null) return;

        if (tour.TourFiles != null)
            foreach (var f in tour.TourFiles)
                await _fileService.DeleteAsync(f.Path);

        _tourRepository.Delete(tour);
        await _tourRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteTourAsync(Guid id)
    {
        var tour = await _tourRepository.GetByIdAsync(id);
        if (tour == null) return;

        if (!tour.IsDeleted)
        {
            tour.IsDeleted = true;
            tour.DeletedAt = DateTime.UtcNow;
            _tourRepository.Update(tour);
            await _tourRepository.SaveChangesAsync();
        }
    }

    public async Task RestoreTourAsync(Guid id)
    {
        var tour = await _tourRepository.GetByIdAsync(id);
        if (tour == null) return;

        if (tour.IsDeleted)
        {
            tour.IsDeleted = false;
            tour.DeletedAt = null;
            _tourRepository.Update(tour);
            await _tourRepository.SaveChangesAsync();
        }
    }
}