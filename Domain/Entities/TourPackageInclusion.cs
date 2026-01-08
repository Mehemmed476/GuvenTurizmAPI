using Domain.Entities.Common;

namespace Domain.Entities;

public class TourPackageInclusion : BaseEntity
{
    public string Description { get; set; } 
    public bool IsIncluded { get; set; } = true;
    
    public Guid TourPackageId { get; set; }
    public TourPackage TourPackage { get; set; }
}