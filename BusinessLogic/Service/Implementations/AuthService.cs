using System.Net; // <--- BU VACİBDİR (WebUtility üçün)
using System.Security.Claims;
using BusinessLogic.DTO.AuthDTOs;
using BusinessLogic.ExternalService.Abstractions;
using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Identity;
using Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Service.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IJwtTokenService _jwt;
    private readonly IEmailService _emailService;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IJwtTokenService jwt,
        IEmailService emailService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwt = jwt;
        _emailService = emailService;
        _configuration = configuration;
    }

    public async Task<AuthResultDTO> RegisterAsync(RegisterDTO dto, IEnumerable<string>? roles = null)
    {
        if (dto.Password != dto.CheckPassword)
            return new AuthResultDTO { Succeeded = false, Errors = new[] { "Şifrələr eyni deyil." } };

        var user = new IdentityUser
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            EmailConfirmed = false // <--- Məcburi false
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            return new AuthResultDTO { Succeeded = false, Errors = result.Errors.Select(e => e.Description) };
        }

        // Rolları əlavə et
        if (roles is null || !roles.Any())
            await _userManager.AddToRoleAsync(user, UserRole.User.ToString());
        else
            await _userManager.AddToRolesAsync(user, roles);

        // --- EMAIL TƏSDİQİ (Düzəldilmiş Hissə) ---
        try 
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            
            // Frontend URL-i
            var frontendUrl = _configuration["ClientUrl"] ?? "http://localhost:3000";
            
            // DƏYİŞİKLİK: HttpUtility yox, WebUtility istifadə edirik
            var encodedToken = WebUtility.UrlEncode(token);
            var link = $"{frontendUrl}/confirm-email?userId={user.Id}&token={encodedToken}";

            var body = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #eee; border-radius: 10px;'>
                    <h2 style='color: #FF5E14;'>Xoş gəldiniz, {user.UserName}!</h2>
                    <p>Qeydiyyatı tamamlamaq üçün zəhmət olmasa aşağıdakı düyməyə klikləyin:</p>
                    <br/>
                    <a href='{link}' style='background-color: #FF5E14; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px; font-weight: bold;'>Hesabı Təsdiqlə</a>
                    <br/><br/>
                    <p style='color: #888; font-size: 12px;'>Əgər düymə işləmirsə, bu linki brauzerə kopyalayın: <br/> {link}</p>
                </div>";

            await _emailService.SendEmailAsync(user.Email, "Hesab Təsdiqi - Güvən Turizm", body);
        }
        catch (Exception ex)
        {
            // Xəta olsa belə istifadəçini silmirik, amma log yazırıq
            Console.WriteLine($"EMAIL XƏTASI: {ex.Message}");
            // İsterseniz burada 'return' edib xəta mesajı qaytara bilərsiniz, 
            // amma istifadəçi artıq yaranıb deyə ona "Email getmədi" demək daha düzgündür.
        }

        return new AuthResultDTO { Succeeded = true, Errors = new[] { "Qeydiyyat tamamlandı! Zəhmət olmasa emailinizi (Spam qovluğunu da) yoxlayın." } };
    }

    public async Task<AuthResultDTO> LoginAsync(LoginDTO dto)
    {
        IdentityUser? user = dto.Email.Contains('@')
            ? await _userManager.FindByEmailAsync(dto.Email)
            : await _userManager.FindByNameAsync(dto.Email);

        if (user is null) return new AuthResultDTO { Succeeded = false, Errors = new[] { "İstifadəçi tapılmadı." } };

        // DƏYİŞİKLİK: Email təsdiqini burada da əl ilə yoxlayırıq (Zəmanət üçün)
        if (!user.EmailConfirmed)
            return new AuthResultDTO { Succeeded = false, Errors = new[] { "Giriş etmək üçün emailinizi təsdiqləməlisiniz!" } };

        var signIn = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
        
        if (!signIn.Succeeded)
        {
            if (signIn.IsLockedOut) return new AuthResultDTO { Succeeded = false, Errors = new[] { "Hesabınız müvəqqəti bloklanıb." } };
            if (signIn.IsNotAllowed) return new AuthResultDTO { Succeeded = false, Errors = new[] { "Email təsdiqlənməyib." } }; // Bu xəta RequireConfirmedEmail=true olanda çıxır
            return new AuthResultDTO { Succeeded = false, Errors = new[] { "Email və ya şifrə yanlışdır." } };
        }

        var roles = await _userManager.GetRolesAsync(user);
        var (token, expires) = await _jwt.GenerateAsync(user, roles, null, dto.RememberMe);

        return new AuthResultDTO { Succeeded = true, Token = token, ExpiresAt = expires };
    }

    public async Task<AuthResultDTO> ConfirmEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return new AuthResultDTO { Succeeded = false, Errors = new[] { "İstifadəçi tapılmadı." } };

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return new AuthResultDTO { Succeeded = true };
        }
        
        return new AuthResultDTO { Succeeded = false, Errors = result.Errors.Select(e => e.Description) };
    }
    public async Task ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return; // Təhlükəsizlik üçün: İstifadəçi yoxdursa da xəta vermirik, sanki göndərildi deyirik.

        // Token yarat
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        
        // Link yarat
        var frontendUrl = _configuration["ClientUrl"] ?? "http://localhost:3000";
        var encodedToken = WebUtility.UrlEncode(token);
        var link = $"{frontendUrl}/reset-password?email={email}&token={encodedToken}";

        var body = $@"
            <h2>Şifrə Yeniləmə</h2>
            <p>Şifrənizi yeniləmək üçün aşağıdakı linkə klikləyin:</p>
            <br/>
            <a href='{link}' style='background-color: #FF5E14; color: white; padding: 12px 24px; text-decoration: none; border-radius: 5px;'>Yeni Şifrə Təyin Et</a>
            <br/><br/>
            <p>Əgər bunu siz etməmisinizsə, bu emaili ignor edin.</p>";

        await _emailService.SendEmailAsync(email, "Şifrəni Sıfırla - Güvən Turizm", body);
    }

    // 2. ŞİFRƏ SIFIRLAMA (Yeni şifrəni tətbiq etmə)
    public async Task<AuthResultDTO> ResetPasswordAsync(ResetPasswordDTO dto)
    {
        if (dto.NewPassword != dto.ConfirmPassword)
            return new AuthResultDTO { Succeeded = false, Errors = new[] { "Şifrələr eyni deyil." } };

        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) 
            return new AuthResultDTO { Succeeded = false, Errors = new[] { "İstifadəçi tapılmadı." } };

        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.NewPassword);
        
        if (result.Succeeded)
            return new AuthResultDTO { Succeeded = true };

        return new AuthResultDTO { Succeeded = false, Errors = result.Errors.Select(e => e.Description) };
    }
}