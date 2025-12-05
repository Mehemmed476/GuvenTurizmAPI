namespace BusinessLogic.DTO.CommonDTOs;

public record FAQPutDTO
{
    public Guid Id { get; set; }
    public string Question { get; set; } = "";
    public string Answer { get; set; } = "";
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}