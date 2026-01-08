using BusinessLogic.DTO.TourPackageDTOs;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.DTO.TourDTOs;

public class TourPutDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int DurationDay { get; set; }
    public int DurationNight { get; set; }
    public DateTime? StartDate { get; set; }

    public List<IFormFile>? NewImages { get; set; } 
    public List<Guid>? ImageIdsToDelete { get; set; } 
    
    public List<TourPackagePostDTO>? Packages { get; set; }
}