using Domain.Entities.Common;

namespace Domain.Entities;

public class FAQ : AuditableEntity
{
    public string Question { get; set; } = "";
    public string Answer { get; set; } = "";
    public int DisplayOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;
}