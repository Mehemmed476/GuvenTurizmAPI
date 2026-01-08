using Domain.Entities.Common;

namespace Domain.Entities;

public class TourFile : BaseEntity
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public string ContentType { get; set; }
    public bool IsMain { get; set; } 

    public Guid TourId { get; set; }
    public Tour Tour { get; set; }
}