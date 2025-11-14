using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.ExternalService.Abstractions;

public interface IJwtTokenService
{
    Task<(string token, DateTime expiresAt)> GenerateAsync(IdentityUser user, IEnumerable<string> roles, IEnumerable<Claim>? extraClaims = null, bool rememberMe = false);
}