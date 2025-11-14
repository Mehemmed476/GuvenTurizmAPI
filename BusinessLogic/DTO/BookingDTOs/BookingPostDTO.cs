using Domain.Enums;

namespace BusinessLogic.DTO.BookingDTOs;

public record BookingPostDTO
{
    public Guid HouseId { get; set; }

    public string? UserId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public BookingStatus Status { get; set; }
}