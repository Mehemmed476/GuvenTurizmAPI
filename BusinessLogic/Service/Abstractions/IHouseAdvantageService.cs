using BusinessLogic.DTO.HouseAdvantageDTOs;

namespace BusinessLogic.Service.Abstractions;

public interface IHouseAdvantageService
{
    Task<ICollection<HouseAdvantageGetDTO>> GetAllAsync();
    Task<ICollection<HouseAdvantageGetDTO>> GetAllActiveAsync();
    Task<HouseAdvantageGetDTO?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(HouseAdvantagePostDTO dto);
    Task UpdateAsync(Guid id, HouseAdvantagePutDTO dto);
    Task DeleteAsync(Guid id);
}