using BusinessLogic.DTO.TourPackageDTOs;
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.DTO.TourDTOs;

public class TourPostDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public int DurationDay { get; set; }
    public int DurationNight { get; set; }
    public DateTime? StartDate { get; set; }
        
    // Dosya Yükleme
    public List<IFormFile>? Files { get; set; } 
        
    // Turu oluştururken paketleri de aynı anda girebilsin diye
    public List<TourPackagePostDTO>? Packages { get; set; }
}