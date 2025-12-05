namespace BusinessLogic.DTO.ReviewDTOs;

public record ReviewPostDTO
{
    public Guid HouseId { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; }
}