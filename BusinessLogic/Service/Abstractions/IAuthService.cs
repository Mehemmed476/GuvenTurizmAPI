using BusinessLogic.DTO.AuthDTOs;

namespace BusinessLogic.Service.Abstractions;

public interface IAuthService
{
    Task<AuthResultDTO> RegisterAsync(RegisterDTO dto, IEnumerable<string>? roles = null);
    Task<AuthResultDTO> LoginAsync(LoginDTO dto);
}