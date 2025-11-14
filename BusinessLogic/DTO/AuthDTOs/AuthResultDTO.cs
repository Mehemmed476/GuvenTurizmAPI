namespace BusinessLogic.DTO.AuthDTOs;

public class AuthResultDTO
{
    public bool Succeeded { get; set; }
    public string? Token { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
}