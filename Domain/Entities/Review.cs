using Domain.Entities.Common;

namespace Domain.Entities;

public class Review : AuditableEntity
{
    public string Text { get; set; } = string.Empty;
    public int Rating { get; set; }
    
    public Guid HouseId { get; set; }
    public House? House { get; set; }

    public string UserId { get; set; } 
    public string UserName { get; set; } 
}