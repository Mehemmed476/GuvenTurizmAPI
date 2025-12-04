using BusinessLogic.ExternalService.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles; // MIME tipləri üçün

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;

    public FilesController(IFileService fileService)
    {
        _fileService = fileService;
    }

    // GET: api/files/houses/covers/sekil.jpg
    [HttpGet("{*path}")]
    public async Task<IActionResult> Get(string path)
    {
        try
        {
            // Faylı oxuyuruq (LocalFileService-dən)
            var stream = await _fileService.OpenReadAsync(path);
            
            // Faylın tipini (MIME) tapırıq (jpg, png və s.)
            var contentType = GetContentType(path);

            return File(stream, contentType);
        }
        catch (FileNotFoundException)
        {
            return NotFound("Fayl tapılmadı.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Xəta: {ex.Message}");
        }
    }

    // Fayl uzantısına görə Content-Type qaytarır
    private string GetContentType(string path)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(path, out var contentType))
        {
            contentType = "application/octet-stream"; // Bilinməyən fayl
        }
        return contentType;
    }
}