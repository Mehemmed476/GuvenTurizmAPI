namespace BusinessLogic.DTO.CategoryDTOs;

public record CategoryPostDTO
{
    public Guid? HouseId { get; set; }

    public string Title { get; set; }

    public string? Description { get; set; }
}