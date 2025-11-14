using AutoMapper;
using BusinessLogic.DTO.CategoryDTOs;
using BusinessLogic.Service.Abstractions;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Service.Implementations;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IHouseRepository _houseRepository;
    private readonly IMapper _mapper;

    public CategoryService(
        IRepository<Category> categoryRepository,
        IHouseRepository houseRepository,
        IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _houseRepository = houseRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<CategoryGetDTO>> GetAllAsync()
    {
        var list = await _categoryRepository.GetAllAsync("Houses");
        return _mapper.Map<ICollection<CategoryGetDTO>>(list);
    }

    public async Task<ICollection<CategoryGetDTO>> GetAllActiveAsync()
    {
        var list = await _categoryRepository
            .GetAllByCondition(x => !x.IsDeleted, "Houses")
            .OrderBy(x => x.Title)
            .ToListAsync();

        return _mapper.Map<ICollection<CategoryGetDTO>>(list);
    }

    public async Task<CategoryGetDTO?> GetByIdAsync(Guid id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id, "Houses");
        if (entity is null) return null;
        return _mapper.Map<CategoryGetDTO>(entity);
    }

    public async Task<Guid> CreateAsync(CategoryPostDTO dto)
    {
        var entity = new Category
        {
            Title = dto.Title,
            Description = dto.Description
        };

        await _categoryRepository.AddAsync(entity);
        await _categoryRepository.SaveChangesAsync();

        if (dto.HouseId.HasValue && dto.HouseId != Guid.Empty)
        {
            var house = await _houseRepository.GetByIdAsync(dto.HouseId.Value);
            if (house is not null && !house.IsDeleted)
            {
                house.CategoryId = entity.Id;
                _houseRepository.Update(house);
                await _houseRepository.SaveChangesAsync();
            }
        }

        return entity.Id;
    }

    public async Task UpdateAsync(Guid id, CategoryPutDTO dto)
    {
        var entity = await _categoryRepository.GetByIdAsync(id, "Houses");
        if (entity is null || entity.IsDeleted) return;

        entity.Title = dto.Title;
        entity.Description = dto.Description;

        _categoryRepository.Update(entity);
        await _categoryRepository.SaveChangesAsync();

        if (dto.HouseId.HasValue && dto.HouseId != Guid.Empty)
        {
            var house = await _houseRepository.GetByIdAsync(dto.HouseId.Value);
            if (house is not null && !house.IsDeleted)
            {
                house.CategoryId = entity.Id;
                _houseRepository.Update(house);
                await _houseRepository.SaveChangesAsync();
            }
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id, "Houses");
        if (entity is null) return;

        if (entity.Houses is not null && entity.Houses.Any())
            throw new InvalidOperationException("Bu kateqoriyaya bağlı evlər var.");

        _categoryRepository.Delete(entity);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task SoftDeleteAsync(Guid id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id);
        if (entity is null || entity.IsDeleted) return;

        entity.IsDeleted = true;
        entity.DeletedAt = DateTime.UtcNow;

        _categoryRepository.Update(entity);
        await _categoryRepository.SaveChangesAsync();
    }

    public async Task RestoreAsync(Guid id)
    {
        var entity = await _categoryRepository.GetByIdAsync(id);
        if (entity is null || !entity.IsDeleted) return;

        entity.IsDeleted = false;
        entity.DeletedAt = null;

        _categoryRepository.Update(entity);
        await _categoryRepository.SaveChangesAsync();
    }
}




