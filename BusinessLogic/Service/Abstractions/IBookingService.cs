using BusinessLogic.DTO.BookingDTOs;

namespace BusinessLogic.Service.Abstractions;

public interface IBookingService
{
    Task<ICollection<BookingGetDTO>> GetAllAsync();
    Task<ICollection<BookingGetDTO>> GetAllActiveAsync();
    Task<ICollection<BookingGetDTO>> GetByHouseIdAsync(Guid houseId);
    Task<ICollection<BookingGetDTO>> GetByUserIdAsync(string userId);
    Task<BookingGetDTO?> GetByIdAsync(Guid id);
    Task<Guid> CreateAsync(BookingPostDTO dto);
    Task UpdateAsync(Guid id, BookingPutDTO dto);
    Task DeleteAsync(Guid id);
    Task SoftDeleteAsync(Guid id);
    Task RestoreAsync(Guid id);
}