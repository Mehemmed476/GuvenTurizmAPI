using Domain.Entities.Common;

namespace Domain.Entities;

public class Tour : AuditableEntity
{
    public string Title { get; set; }
    public string Description { get; set; } 
    public string Location { get; set; }
    public int DurationDay { get; set; } 
    public int DurationNight { get; set; } 
    public DateTime? StartDate { get; set; } 
    public bool IsActive { get; set; } = true;
    
    public ICollection<TourFile> TourFiles { get; set; }
    public ICollection<TourPackage> TourPackages { get; set; }
}