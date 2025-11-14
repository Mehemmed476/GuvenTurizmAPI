using BusinessLogic.DTO.HouseAdvantageDTOs;
using BusinessLogic.Service.Abstractions;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace BusinessLogic.Service.Implementations;

public class HouseAdvantageService : IHouseAdvantageService
{
    private readonly IHouseAdvantageRepository _repo;

    public HouseAdvantageService(IHouseAdvantageRepository repo)
    {
        _repo = repo;
    }

    public async Task<ICollection<HouseAdvantageGetDTO>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return list
            .OrderBy(x => x.Title)
            .Select(x => new HouseAdvantageGetDTO
            {
                Id = x.Id,
                Title = x.Title,
                IsDeleted = x.IsDeleted,
            })
            .ToList();
    }

    public async Task<ICollection<HouseAdvantageGetDTO>> GetAllActiveAsync()
    {
        var list = await _repo.GetAllAsync();
        return list
            .Where(x => !x.IsDeleted)
            .OrderBy(x => x.Title)
            .Select(x => new HouseAdvantageGetDTO
            {
                Id = x.Id,
                Title = x.Title,
                IsDeleted = x.IsDeleted,
            })
            .ToList();
    }

    public async Task<HouseAdvantageGetDTO?> GetByIdAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return null;

        return new HouseAdvantageGetDTO
        {
            Id = entity.Id,
            Title = entity.Title,
            IsDeleted = entity.IsDeleted,
        };
    }

    public async Task<Guid> CreateAsync(HouseAdvantagePostDTO dto)
    {
        var entity = new HouseAdvantage
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            IsDeleted = false,
        };

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, HouseAdvantagePutDTO dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException("Üstünlük tapılmadı.");

        entity.Title = dto.Title;
        _repo.Update(entity);
        await _repo.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity is null) return;

        _repo.Delete(entity);
        await _repo.SaveChangesAsync();
    }
}
