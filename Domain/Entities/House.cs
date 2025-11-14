using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities;

public class House : AuditableEntity
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public byte NumberOfRooms { get; set; } = 0;
    public byte NumberOfBeds { get; set; } = 0;
    public byte NumberOfFloors { get; set; } = 0;
    public int Field { get; set; } = 0;
    public string Address { get; set; } = "";
    public string City { get; set; } = "";
    public string GoogleMapsCode { get; set; } = "";
    public string CoverImage { get; set; } = "";
    
    
    public Guid CategoryId { get; set; } = Guid.Empty;
    
    public Category? Category { get; set; }
    public ICollection<HouseFile> Images { get; set; } = new List<HouseFile>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<HouseHouseAdvantageRel> HouseHouseAdvantageRels { get; set; } = new List<HouseHouseAdvantageRel>();
}