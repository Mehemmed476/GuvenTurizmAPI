using Domain.Entities.Common;

namespace Domain.Entities;

public class Setting : BaseEntity
{
    public string Key { get; set; }
    public string Value { get; set; }
}