using Domain.Entities.Common;

namespace Domain.Entities;

public class HouseAdvantage : BaseEntity
{
    public string Title { get; set; }
    public ICollection<HouseHouseAdvantageRel> HouseHouseAdvantageRels { get; set; } = new List<HouseHouseAdvantageRel>();
}