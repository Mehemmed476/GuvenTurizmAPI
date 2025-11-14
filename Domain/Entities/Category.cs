using Domain.Entities.Common;

namespace Domain.Entities;

public class Category : AuditableEntity
{
    public string Title { get; set; } = "";
    public string? Description { get; set; } = "";
    
    public ICollection<House> Houses { get; set; } = new List<House>();
}