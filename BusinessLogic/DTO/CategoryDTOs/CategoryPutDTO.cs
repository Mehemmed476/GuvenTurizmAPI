namespace BusinessLogic.DTO.CategoryDTOs;

public record CategoryPutDTO
{
    public Guid Id { get; set; }

    public Guid? HouseId { get; set; }

    public string Title { get; set; } = "";
    public string? Description { get; set; } = "";
}