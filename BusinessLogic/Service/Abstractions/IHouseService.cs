using BusinessLogic.DTO.HouseDTOs;

namespace BusinessLogic.Service.Abstractions;

public interface IHouseService 
{
    Task<ICollection<HouseGetDTO>> GetAllHousesAsync();
    
    // DƏYİŞİKLİK: Pagination parametrləri əlavə edildi
    Task<ICollection<HouseGetDTO>> GetAllActiveHousesAsync(int page = 1, int size = 9);
    
    Task<ICollection<HouseGetDTO>> GetHousesByCategoryIdAsync(Guid categoryId);
    Task<HouseGetDTO> GetHouseByIdAsync(Guid houseId);
    
    Task CreateHouseAsync(HousePostDTO housePostDTO);
    Task UpdateHouseAsync(Guid houseId, HousePutDTO housePutDTO);
    Task DeleteHouseAsync(Guid houseId);
    Task SoftDetectedHouseAsync(Guid houseId);
    Task RestoreHouseAsync(Guid houseId);
}