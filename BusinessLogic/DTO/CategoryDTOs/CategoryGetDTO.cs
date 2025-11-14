using Domain.Entities;

namespace BusinessLogic.DTO.CategoryDTOs;

public record CategoryGetDTO
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

    public string Title { get; set; }

    public string? Description { get; set; }

    public ICollection<House> Houses { get; set; } =  new List<House>();
}