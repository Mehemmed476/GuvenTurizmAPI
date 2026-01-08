using BusinessLogic.DTO.TourPackageInclusionDTOs;

namespace BusinessLogic.DTO.TourPackageDTOs;

public class TourPackageGetDTO
{
    public Guid Id { get; set; }
    public string PackageName { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
    public Guid TourId { get; set; }
        
    public List<TourPackageInclusionGetDTO> Inclusions { get; set; }
}
