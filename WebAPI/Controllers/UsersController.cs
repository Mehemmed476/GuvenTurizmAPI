using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireAdmin")] // Yalnız Adminlər görə bilər
public class UsersController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    // Bütün istifadəçiləri gətir
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userManager.Users.ToListAsync();
        
        // Həssas məlumatları (PasswordHash və s.) göndərmirik
        var result = users.Select(u => new 
        {
            u.Id,
            u.UserName,
            u.Email,
            u.PhoneNumber,
            u.EmailConfirmed
        });

        return Ok(result);
    }

    // İstifadəçini sil
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user is null) return NotFound("İstifadəçi tapılmadı.");

        // Adminin özünü silməsinə icazə verməyək (Opsional)
        if (user.Email == "admin@site.com") 
            return BadRequest("Admin istifadəçisi silinə bilməz.");

        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return NoContent();
    }
}