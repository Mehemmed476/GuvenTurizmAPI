using Domain.Enums;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.DTO.HouseDTOs;

public record HouseGetDTO
{
    public Guid Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public string? DeletedBy { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public byte NumberOfRooms { get; set; }
    public byte NumberOfBeds { get; set; }
    public byte NumberOfFloors { get; set; }
    public int Field { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string GoogleMapsCode { get; set; }
    public string CoverImage { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<HouseFile> Images { get; set; } = new List<HouseFile>();
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    public ICollection<HouseHouseAdvantageRel> HouseHouseAdvantageRels { get; set; } = new List<HouseHouseAdvantageRel>();
}