using Domain.Entities.Common;

namespace Domain.Entities;

public class HouseFile : BaseEntity
{
    public Guid HouseId { get; set; }
    public string Image { get; set; } = "";
    
    public House? House { get; set; }
}