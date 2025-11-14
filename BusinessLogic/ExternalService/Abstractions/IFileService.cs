
using Microsoft.AspNetCore.Http;

namespace BusinessLogic.ExternalService.Abstractions;

public interface IFileService
{
    Task<string> SaveAsync(IFormFile file, string folder, bool isPublic = true, CancellationToken ct = default);
    Task DeleteAsync(string key, CancellationToken ct = default);
    string GetPublicUrl(string key);
    Task<Stream> OpenReadAsync(string key, CancellationToken ct = default);
}