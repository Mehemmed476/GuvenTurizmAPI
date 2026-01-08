using BusinessLogic.DTO.TourPackageDTOs;

namespace BusinessLogic.DTO.TourDTOs;

public class TourGetDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int DurationDay { get; set; }
    public int DurationNight { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime CreatedAt { get; set; } // AuditableEntity'den
        
    // İlişkiler
    public List<string> ImageUrls { get; set; } // Resim yolları
    public List<TourPackageGetDTO> Packages { get; set; } // Paketler
}