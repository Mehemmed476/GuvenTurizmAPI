using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.DTO.HouseDTOs;

public record HousePostDTO
{
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public decimal Price { get; set; }
    public byte NumberOfRooms { get; set; }
    public byte NumberOfBeds { get; set; }
    public byte NumberOfFloors { get; set; }
    public int Field { get; set; }
    public string Address { get; set; } = "";
    public string City { get; set; } = "";
    public string GoogleMapsCode { get; set; } = "";
    public IFormFile? CoverImage { get; set; }
    public Guid CategoryId { get; set; }
    public ICollection<IFormFile>? Images { get; set; } = new List<IFormFile>();
    public ICollection<Guid>? AdvantageIds { get; set; } = new List<Guid>();
}