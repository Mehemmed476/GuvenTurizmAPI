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

    public TourService(
        ITourRepository tourRepository,
        IFileService fileService,
        IMapper mapper)
    {
        _tourRepository = tourRepository;
        _fileService = fileService;
        _mapper = mapper;
    }

    public async Task<ICollection<TourGetDTO>> GetAllToursAsync()
    {
        var tours = await _tourRepository.GetAllAsync(
            "TourFiles",
            "TourPackages",
            "TourPackages.Inclusions"
        );
        return _mapper.Map<ICollection<TourGetDTO>>(tours);
    }

    public async Task<ICollection<TourGetDTO>> GetAllActiveToursAsync(int page = 1, int size = 9)
    {
        // HouseService-dəki məntiqlə eyni: IsDeleted olmayanlar
        var query = _tourRepository
            .GetAllByCondition(t => !t.IsDeleted && t.IsActive,
                "TourFiles",
                "TourPackages",
                "TourPackages.Inclusions");

        var tours = await query
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();

        return _mapper.Map<ICollection<TourGetDTO>>(tours);
    }

    public async Task<ICollection<TourGetDTO>> GetToursByLocationAsync(string location)
    {
        var tours = await _tourRepository
            .GetAllByCondition(t => !t.IsDeleted && t.IsActive && t.Location.Contains(location),
                "TourFiles",
                "TourPackages",
                "TourPackages.Inclusions")
            .ToListAsync();

        return _mapper.Map<ICollection<TourGetDTO>>(tours);
    }

    public async Task<TourGetDTO> GetTourByIdAsync(Guid id)
    {
        // BURASI ÇOX VACİBDİR: Paketləri və İçindəkiləri mütləq Include edirik
        var tour = await _tourRepository.GetByIdAsync(id,
            "TourFiles",
            "TourPackages",
            "TourPackages.Inclusions");

        return _mapper.Map<TourGetDTO>(tour);
    }

    public async Task CreateTourAsync(TourPostDTO dto)
    {
        var tour = _mapper.Map<Tour>(dto);
        tour.IsActive = true; 

        // 1. Şəkillər üçün siyahını başlat (Əks halda NullReference xətası verir)
        tour.TourFiles = new List<TourFile>(); 

        if (dto.Files is not null && dto.Files.Count > 0)
        {
            foreach (var file in dto.Files)
            {
                var key = await _fileService.SaveAsync(file, "tours/gallery");
            
                // İlk şəkil avtomatik "Main" (Kapak) şəkli olsun
                bool isFirst = tour.TourFiles.Count == 0;
            
                tour.TourFiles.Add(new TourFile 
                { 
                    TourId = tour.Id, // (Bəzən EF Core bunu avtomatik edir, amma əllə yazmaq daha etibarlıdır)
                    Path = key, 
                    FileName = file.FileName, 
                    ContentType = file.ContentType,
                    IsMain = isFirst 
                });
            }
        }

        // 2. Paketlər üçün siyahını başlat (Ən vacib hissə buradır!)
        tour.TourPackages = new List<TourPackage>();

        if (dto.Packages is not null && dto.Packages.Count > 0)
        {
            foreach (var pkgDto in dto.Packages)
            {
                var newPackage = new TourPackage
                {
                    PackageName = pkgDto.PackageName,
                    Price = pkgDto.Price,
                    DiscountPrice = pkgDto.DiscountPrice,
                    Inclusions = new List<TourPackageInclusion>() // İçindəkiləri də başlat
                };

                if (pkgDto.Inclusions is not null)
                {
                    foreach (var inc in pkgDto.Inclusions)
                    {
                        newPackage.Inclusions.Add(new TourPackageInclusion { Description = inc });
                    }
                }
                // Artıq tour.TourPackages null deyil, rahat əlavə edə bilərik
                tour.TourPackages.Add(newPackage);
            }
        }

        await _tourRepository.AddAsync(tour);
        await _tourRepository.SaveChangesAsync();
    }

    public async Task UpdateTourAsync(Guid id, TourPutDTO dto)
    {
        var tour = await _tourRepository.GetByIdAsync(id,
            "TourFiles",
            "TourPackages",
            "TourPackages.Inclusions");

        if (tour is null) return;

        // Sadə sahələrin yenilənməsi
        tour.Title = dto.Title;
        tour.Description = dto.Description;
        tour.Location = dto.Location;
        tour.DurationDay = dto.DurationDay;
        tour.DurationNight = dto.DurationNight;
        tour.StartDate = dto.StartDate;

        // --- FAYL SİLMƏ (HouseService-dəki kimi) ---
        if (dto.ImageIdsToDelete is not null && dto.ImageIdsToDelete.Count > 0)
        {
            var toRemove = tour.TourFiles
                .Where(x => dto.ImageIdsToDelete.Contains(x.Id))
                .ToList();

            foreach (var img in toRemove)
            {
                await _fileService.DeleteAsync(img.Path);
                tour.TourFiles.Remove(img);
            }
        }

        // --- YENİ FAYL ƏLAVƏ ETMƏ (HouseService-dəki kimi) ---
        if (dto.NewImages is not null && dto.NewImages.Count > 0)
        {
            foreach (var upload in dto.NewImages)
            {
                var key = await _fileService.SaveAsync(upload, "tours/gallery");
                tour.TourFiles.Add(new TourFile 
                { 
                    TourId = tour.Id, 
                    Path = key, 
                    FileName = upload.FileName, 
                    ContentType = upload.ContentType,
                    IsMain = false // Sonradan əlavə olunanlar main olmasın
                });
            }
        }

        // --- PAKET YENİLƏMƏ ---
        // HouseService-də Advantage-lər ID ilə işləyir, amma Tur Paketləri bütöv obyektdir.
        // Ən təmiz yol: Mövcud paketləri silib, formdan gələnləri yenidən yaratmaqdır.
        if (dto.Packages is not null)
        {
            // Mövcud paketləri təmizlə
            tour.TourPackages.Clear();

            // Yenilərini əlavə et
            foreach (var pkgDto in dto.Packages)
            {
                var newPkg = new TourPackage
                {
                    PackageName = pkgDto.PackageName,
                    Price = pkgDto.Price,
                    DiscountPrice = pkgDto.DiscountPrice,
                    Inclusions = new List<TourPackageInclusion>()
                };

                if (pkgDto.Inclusions is not null)
                {
                    foreach (var inc in pkgDto.Inclusions)
                    {
                        newPkg.Inclusions.Add(new TourPackageInclusion { Description = inc });
                    }
                }
                tour.TourPackages.Add(newPkg);
            }
        }

        _tourRepository.Update(tour);
        await _tourRepository.SaveChangesAsync();
    }

    public async Task DeleteTourAsync(Guid id)
    {
        var tour = await _tourRepository.GetByIdAsync(id, 
            "TourFiles", 
            "TourPackages");

        if (tour is null) return;

        // Fiziki faylları sil
        if (tour.TourFiles is not null)
        {
            foreach (var img in tour.TourFiles)
                await _fileService.DeleteAsync(img.Path);
        }

        // Paketləri təmizlə (House-da Relations.Clear() kimi)
        if (tour.TourPackages is not null)
        {
            tour.TourPackages.Clear();
        }

        _tourRepository.Delete(tour);
        await _tourRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteTourAsync(Guid id)
    {
        var tour = await _tourRepository.GetByIdAsync(id);
        if (tour is null) return;

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
        if (tour is null) return;

        if (tour.IsDeleted)
        {
            tour.IsDeleted = false;
            tour.DeletedAt = null;
            _tourRepository.Update(tour);
            await _tourRepository.SaveChangesAsync();
        }
    }
}