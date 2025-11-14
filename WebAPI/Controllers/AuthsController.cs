using System.Security.Claims;
using BusinessLogic.DTO.AuthDTOs;
using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthsController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var res = await _auth.RegisterAsync(dto);
            return res.Succeeded ? Ok(res) : BadRequest(res);
        }

        [HttpPost("register-with-roles")]
        [Authorize(Policy = "RequireAdmin")]
        public async Task<IActionResult> RegisterWithRoles([FromBody] RegisterDTO dto, [FromQuery] string[] roles)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var res = await _auth.RegisterAsync(dto, roles);
            return res.Succeeded ? Ok(res) : BadRequest(res);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var res = await _auth.LoginAsync(dto);
            return res.Succeeded ? Ok(res) : Unauthorized(res);
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name ?? "";
            var email = User.FindFirstValue(ClaimTypes.Email) ?? "";
            var roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToArray();

            return Ok(new
            {
                userId,
                userName,
                email,
                roles
            });
        }
    }
}
