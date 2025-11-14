namespace Domain.Entities.Common;

public abstract class AuditableEntity : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
    public DateTime? ModifiedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public string? DeletedBy { get; set; }
}