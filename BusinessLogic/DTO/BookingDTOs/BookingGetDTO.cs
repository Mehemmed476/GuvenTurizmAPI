using Domain.Entities;
using Domain.Enums;

namespace BusinessLogic.DTO.BookingDTOs;

public record BookingGetDTO
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public string? DeletedBy { get; set; }

    public Guid HouseId { get; set; }
    public House? House { get; set; }

    public string? UserId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public BookingStatus Status { get; set; }
}