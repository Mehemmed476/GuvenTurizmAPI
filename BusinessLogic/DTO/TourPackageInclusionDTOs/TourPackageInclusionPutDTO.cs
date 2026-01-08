namespace BusinessLogic.DTO.TourPackageInclusionDTOs;

public class TourPackageInclusionPutDTO
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public bool IsIncluded { get; set; }
}