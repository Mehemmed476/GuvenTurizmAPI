using BusinessLogic.DTO.HouseDTOs;
using Domain.Entities;

namespace BusinessLogic.Service.Abstractions;

public interface IHouseService 
{
    Task<ICollection<HouseGetDTO>> GetAllHousesAsync();
    Task<ICollection<HouseGetDTO>> GetAllActiveHousesByAsync();
    Task<ICollection<HouseGetDTO>> GetHousesByCategoryIdAsync(Guid categoryId);
    Task<HouseGetDTO> GetHouseByIdAsync(Guid houseId);
    
    Task CreateHouseAsync(HousePostDTO housePostDTO);
    Task UpdateHouseAsync(Guid houseId, HousePutDTO housePutDTO);
    Task DeleteHouseAsync(Guid houseId);
    Task SoftDetectedHouseAsync(Guid houseId);
    Task RestoreHouseAsync(Guid houseId);
}