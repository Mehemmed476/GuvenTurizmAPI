namespace BusinessLogic.DTO.TourPackageDTOs;

public class TourPackagePostDTO
{
    public string PackageName { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }

    public List<string> Inclusions { get; set; } 
}

