using Domain.Entities.Common;

namespace Domain.Entities;

public class HouseHouseAdvantageRel : BaseEntity
{
    public Guid HouseId { get; set; }
    public House? House { get; set; }
    public Guid HouseAdvantageId { get; set; }
    public HouseAdvantage? HouseAdvantage { get; set; }
}