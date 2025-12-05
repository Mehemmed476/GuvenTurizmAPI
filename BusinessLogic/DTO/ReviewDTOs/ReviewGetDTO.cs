namespace BusinessLogic.DTO.ReviewDTOs;

public record ReviewGetDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}