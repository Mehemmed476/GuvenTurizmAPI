using System.Security.Claims;
using BusinessLogic.DTO.AuthDTOs;
using BusinessLogic.ExternalService.Abstractions;
using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Identity;
using Domain.Enums;

namespace BusinessLogic.Service.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IJwtTokenService _jwt;

    public AuthService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IJwtTokenService jwt)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwt = jwt;
    }

    public async Task<AuthResultDTO> RegisterAsync(RegisterDTO dto, IEnumerable<string>? roles = null)
    {
        var user = new IdentityUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            EmailConfirmed = true
        };

        if (dto.Password != dto.CheckPassword)
        {
            return new AuthResultDTO { Succeeded = false, Errors = new[] { "Şifrələr eyni deyil." } };
        }

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return new AuthResultDTO
            {
                Succeeded = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        IdentityResult roleResult;
        if (roles is null || !roles.Any())
        {
            roleResult = await _userManager.AddToRoleAsync(user, UserRole.User.ToString());
        }
        else
        {
            roleResult = await _userManager.AddToRolesAsync(user, roles);
        }

        if (!roleResult.Succeeded)
        {
            return new AuthResultDTO
            {
                Succeeded = false,
                Errors = roleResult.Errors.Select(e => e.Description)
            };
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var (token, expires) = await _jwt.GenerateAsync(user, userRoles);

        return new AuthResultDTO { Succeeded = true, Token = token, ExpiresAt = expires };
    }

    public async Task<AuthResultDTO> LoginAsync(LoginDTO dto)
    {
        IdentityUser? user;

        user = dto.Email.Contains('@')
            ? await _userManager.FindByEmailAsync(dto.Email)
            : await _userManager.FindByNameAsync(dto.Email);

        if (user is null)
        {
            return new AuthResultDTO { Succeeded = false, Errors = new[] { "İstifadəçi tapılmadı." } };
        }

        var signIn = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
        if (!signIn.Succeeded)
        {
            return new AuthResultDTO { Succeeded = false, Errors = new[] { "Email və ya şifrə yanlışdır." } };
        }

        var roles = await _userManager.GetRolesAsync(user);
        var extraClaims = new List<Claim>();
        var (token, expires) = await _jwt.GenerateAsync(user, roles, extraClaims, dto.RememberMe);

        return new AuthResultDTO { Succeeded = true, Token = token, ExpiresAt = expires };
    }
}
