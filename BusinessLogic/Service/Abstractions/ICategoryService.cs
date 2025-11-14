using BusinessLogic.DTO.CategoryDTOs;

namespace BusinessLogic.Service.Abstractions;

public interface ICategoryService
{
    Task<ICollection<CategoryGetDTO>> GetAllAsync();
    Task<ICollection<CategoryGetDTO>> GetAllActiveAsync();
    Task<CategoryGetDTO?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(CategoryPostDTO dto);
    Task UpdateAsync(Guid id, CategoryPutDTO dto);
    Task DeleteAsync(Guid id);
    Task SoftDeleteAsync(Guid id);
    Task RestoreAsync(Guid id);
}
