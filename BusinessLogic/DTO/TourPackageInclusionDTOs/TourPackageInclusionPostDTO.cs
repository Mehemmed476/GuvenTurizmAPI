namespace BusinessLogic.DTO.TourPackageInclusionDTOs;

public class TourPackageInclusionPostDTO
{
    public Guid TourPackageId { get; set; }
    public string Description { get; set; }
    public bool IsIncluded { get; set; } = true;
}
