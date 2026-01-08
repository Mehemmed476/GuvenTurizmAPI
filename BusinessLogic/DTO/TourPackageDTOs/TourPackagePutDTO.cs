namespace BusinessLogic.DTO.TourPackageDTOs;

public class TourPackagePutDTO
{
    public Guid Id { get; set; }
    public string PackageName { get; set; }
    public decimal Price { get; set; }
    public decimal? DiscountPrice { get; set; }
        
    public List<string> Inclusions { get; set; }
}

