using AutoMapper;
using BusinessLogic.DTO.HouseDTOs;
using BusinessLogic.ExternalService.Abstractions;
using BusinessLogic.Service.Abstractions;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Service.Implementations;

public class HouseService : IHouseService
{
    private readonly IHouseRepository _houseRepository;
    private readonly IRepository<HouseHouseAdvantageRel> _houseAdvRelRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public HouseService(
        IHouseRepository houseRepository,
        IRepository<HouseHouseAdvantageRel> houseAdvRelRepository,
        IMapper mapper,
        IFileService fileService)
    {
        _houseRepository = houseRepository;
        _houseAdvRelRepository = houseAdvRelRepository;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<ICollection<HouseGetDTO>> GetAllHousesAsync()
    {
        var houses = await _houseRepository.GetAllAsync(
            "Category",
            "Images",
            "Bookings",
            "HouseHouseAdvantageRels",
            "HouseHouseAdvantageRels.HouseAdvantage"
        );
        return _mapper.Map<ICollection<HouseGetDTO>>(houses);
    }

    public async Task<ICollection<HouseGetDTO>> GetAllActiveHousesByAsync()
    {
        var houses = await _houseRepository
            .GetAllByCondition(h => !h.IsDeleted,
                "Category",
                "Images",
                "Bookings",
                "HouseHouseAdvantageRels",
                "HouseHouseAdvantageRels.HouseAdvantage")
            .ToListAsync();

        return _mapper.Map<ICollection<HouseGetDTO>>(houses);
    }

    public async Task<ICollection<HouseGetDTO>> GetHousesByCategoryIdAsync(Guid categoryId)
    {
        var houses = await _houseRepository
            .GetAllByCondition(h => !h.IsDeleted && h.CategoryId == categoryId,
                "Category",
                "Images",
                "Bookings",
                "HouseHouseAdvantageRels",
                "HouseHouseAdvantageRels.HouseAdvantage")
            .ToListAsync();

        return _mapper.Map<ICollection<HouseGetDTO>>(houses);
    }

    public async Task<HouseGetDTO> GetHouseByIdAsync(Guid houseId)
    {
        var house = await _houseRepository.GetByIdAsync(houseId,
            "Category",
            "Images",
            "Bookings",
            "HouseHouseAdvantageRels",
            "HouseHouseAdvantageRels.HouseAdvantage");

        return _mapper.Map<HouseGetDTO>(house);
    }

    public async Task CreateHouseAsync(HousePostDTO dto)
    {
        var house = _mapper.Map<House>(dto);

        if (dto.CoverImage is not null)
        {
            var coverKey = await _fileService.SaveAsync(dto.CoverImage, "houses/covers");
            house.CoverImage = coverKey;
        }

        if (dto.Images is not null && dto.Images.Count > 0)
        {
            foreach (var img in dto.Images)
            {
                var key = await _fileService.SaveAsync(img, "houses/gallery");
                house.Images.Add(new HouseFile { HouseId = house.Id, Image = key });
            }
        }

        if (dto.AdvantageIds is not null && dto.AdvantageIds.Count > 0)
        {
            foreach (var advId in dto.AdvantageIds.Distinct())
            {
                house.HouseHouseAdvantageRels.Add(new HouseHouseAdvantageRel
                {
                    HouseId = house.Id,
                    HouseAdvantageId = advId
                });
            }
        }

        await _houseRepository.AddAsync(house);
        await _houseRepository.SaveChangesAsync();
    }

    public async Task UpdateHouseAsync(Guid houseId, HousePutDTO dto)
    {
        var house = await _houseRepository.GetByIdAsync(houseId,
            "Images",
            "HouseHouseAdvantageRels");

        if (house is null) return;

        house.Title = dto.Title;
        house.Description = dto.Description;
        house.Price = dto.Price;
        house.NumberOfRooms = dto.NumberOfRooms;
        house.NumberOfBeds = dto.NumberOfBeds;
        house.NumberOfFloors = dto.NumberOfFloors;
        house.Field = dto.Field;
        house.Address = dto.Address;
        house.City = dto.City;
        house.GoogleMapsCode = dto.GoogleMapsCode;
        house.CategoryId = dto.CategoryId;

        if (dto.CoverImage is not null)
        {
            if (!string.IsNullOrWhiteSpace(house.CoverImage))
                await _fileService.DeleteAsync(house.CcoverImageSafe());

            var newCoverKey = await _fileService.SaveAsync(dto.CoverImage, "houses/covers");
            house.CoverImage = newCoverKey;
        }

        if (dto.ImageIdsToDelete is not null && dto.ImageIdsToDelete.Count > 0)
        {
            var toRemove = house.Images
                .Where(x => dto.ImageIdsToDelete.Contains(x.Id))
                .ToList();

            foreach (var img in toRemove)
            {
                await _fileService.DeleteAsync(img.Image);
                house.Images.Remove(img);
            }
        }

        if (dto.NewImages is not null && dto.NewImages.Count > 0)
        {
            foreach (var upload in dto.NewImages)
            {
                var key = await _fileService.SaveAsync(upload, "houses/gallery");
                house.Images.Add(new HouseFile { HouseId = house.Id, Image = key });
            }
        }

        if (dto.AdvantageIds is not null)
        {
            var currentIds = house.HouseHouseAdvantageRels.Select(r => r.HouseAdvantageId).ToHashSet();
            var desiredIds = dto.AdvantageIds.ToHashSet();

            var toAdd = desiredIds.Except(currentIds).ToList();
            var toDelete = currentIds.Except(desiredIds).ToList();

            if (toDelete.Count > 0)
            {
                var relsToDelete = house.HouseHouseAdvantageRels
                    .Where(r => toDelete.Contains(r.HouseAdvantageId))
                    .ToList();

                foreach (var rel in relsToDelete)
                    house.HouseHouseAdvantageRels.Remove(rel);
            }

            foreach (var addId in toAdd)
            {
                house.HouseHouseAdvantageRels.Add(new HouseHouseAdvantageRel
                {
                    HouseId = house.Id,
                    HouseAdvantageId = addId
                });
            }
        }

        _houseRepository.Update(house);
        await _houseRepository.SaveChangesAsync();
    }

    public async Task DeleteHouseAsync(Guid houseId)
    {
        var house = await _houseRepository.GetByIdAsync(houseId,
            "Images",
            "HouseHouseAdvantageRels");

        if (house is null) return;

        if (!string.IsNullOrWhiteSpace(house.CoverImage))
            await _fileService.DeleteAsync(house.CcoverImageSafe());

        if (house.Images is not null)
        {
            foreach (var img in house.Images)
                await _fileService.DeleteAsync(img.Image);
        }

        if (house.HouseHouseAdvantageRels.Count > 0)
        {
            house.HouseHouseAdvantageRels.Clear();
        }

        _houseRepository.Delete(house);
        await _houseRepository.SaveChangesAsync();
    }

    public async Task SoftDetectedHouseAsync(Guid houseId)
    {
        var house = await _houseRepository.GetByIdAsync(houseId);
        if (house is null) return;

        if (!house.IsDeleted)
        {
            house.IsDeleted = true;
            house.DeletedAt = DateTime.UtcNow;
            _houseRepository.Update(house);
            await _houseRepository.SaveChangesAsync();
        }
    }

    public async Task RestoreHouseAsync(Guid houseId)
    {
        var house = await _houseRepository.GetByIdAsync(houseId);
        if (house is null) return;

        if (house.IsDeleted)
        {
            house.IsDeleted = false;
            house.DeletedAt = null;
            _houseRepository.Update(house);
            await _houseRepository.SaveChangesAsync();
        }
    }
}

internal static class HouseFileSafetyExtensions
{
    public static string CcoverImageSafe(this House h)
        => string.IsNullOrWhiteSpace(h.CoverImage) ? "" : h.CoverImage;
}
