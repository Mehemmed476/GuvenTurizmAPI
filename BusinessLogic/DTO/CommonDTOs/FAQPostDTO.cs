namespace BusinessLogic.DTO.CommonDTOs;

public record FAQPostDTO
{
    public string Question { get; set; } = "";
    public string Answer { get; set; } = "";
    public int DisplayOrder { get; set; }
}