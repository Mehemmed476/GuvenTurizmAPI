using Domain.Entities.Common;

namespace Domain.Entities;

public class TourPackage : BaseEntity
{
    public string PackageName { get; set; } // Örn: Standart Paket
    public decimal Price { get; set; } // Fiyat
    public decimal? DiscountPrice { get; set; } // İndirimli Fiyat
        
    // İlişki (Hangi tura ait)
    public Guid TourId { get; set; }
    public Tour Tour { get; set; }

    // Bir paketin sunduğu imkanlar
    public ICollection<TourPackageInclusion> Inclusions { get; set; }
}