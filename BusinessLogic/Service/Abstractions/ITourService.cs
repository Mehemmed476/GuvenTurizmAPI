using BusinessLogic.DTO.TourDTOs;

namespace BusinessLogic.Service.Abstractions;

public interface ITourService
{
    Task<ICollection<TourGetDTO>> GetAllToursAsync();
    Task<ICollection<TourGetDTO>> GetAllActiveToursAsync(int page = 1, int size = 9);
    Task<ICollection<TourGetDTO>> GetToursByLocationAsync(string location);
    Task<TourGetDTO> GetTourByIdAsync(Guid id);
    Task CreateTourAsync(TourPostDTO dto);
    Task UpdateTourAsync(Guid id, TourPutDTO dto);
    Task DeleteTourAsync(Guid id);
    Task SoftDeleteTourAsync(Guid id);
    Task RestoreTourAsync(Guid id);
}