using Domain.Enums;

namespace BusinessLogic.DTO.BookingDTOs;

public record BookingPutDTO
{
    public Guid Id { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public BookingStatus Status { get; set; }
}