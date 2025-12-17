using BusinessLogic.ExternalService.Abstractions;
using BusinessLogic.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BusinessLogic.ExternalService.Implementations;

public class LocalFileService : IFileService
{
    private readonly LocalStorageOptions _opt;

    public LocalFileService(IOptions<LocalStorageOptions> options)
    {
        _opt = options.Value;
        Directory.CreateDirectory(_opt.RootPath);
    }


    public async Task<string> SaveAsync(IFormFile file, string folder, bool isPublic = true, CancellationToken ct = default)
    {
        if (file.Length <= 0) throw new InvalidOperationException("Boş dosya.");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".pdf" };
        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(ext))
        {
            throw new Exception($"GÜVENLİK UYARISI: {ext} uzantılı dosya yüklenemez!");
        }

        var name = $"{Guid.NewGuid():N}{ext}";
        var key = Path.Combine(folder, name).Replace('\\', '/');

        var full = Path.Combine(_opt.RootPath, key);
        Directory.CreateDirectory(Path.GetDirectoryName(full)!);

        await using var fs = new FileStream(full, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await file.CopyToAsync(fs, ct);
        return key;
    }

    public Task DeleteAsync(string key, CancellationToken ct = default)
    {
        var full = Path.Combine(_opt.RootPath, key);
        if (File.Exists(full)) File.Delete(full);
        return Task.CompletedTask;
    }

    public string GetPublicUrl(string key) => $"{_opt.PublicBaseUrl}{key}".Replace("//","/");

    public Task<Stream> OpenReadAsync(string key, CancellationToken ct = default)
    {
        var full = Path.Combine(_opt.RootPath, key);
        if (!File.Exists(full)) throw new FileNotFoundException("Tapılmadı", key);
        return Task.FromResult<Stream>(new FileStream(full, FileMode.Open, FileAccess.Read, FileShare.Read));
    }
}