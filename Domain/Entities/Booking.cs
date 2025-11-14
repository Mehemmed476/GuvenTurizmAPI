using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Booking : AuditableEntity
{
    public Guid HouseId { get; set; }
    public House? House { get; set; }

    public string? UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public BookingStatus Status { get; set; } = BookingStatus.Pending;
}