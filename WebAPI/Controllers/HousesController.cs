using BusinessLogic.DTO.HouseDTOs;
using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HousesController : ControllerBase
{
    private readonly IHouseService _service;

    public HousesController(IHouseService service)
    {
        _service = service;
    }

    // AZ: Bütün evlər (admin)
    [HttpGet]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllHousesAsync();
        return Ok(data);
    }

    [HttpGet("active")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllActive([FromQuery] int page = 1, [FromQuery] int size = 9)
    {
        var data = await _service.GetAllActiveHousesAsync(page, size);
        return Ok(data);
    }

    // AZ: Kateqoriyaya görə aktiv evlər (hamı)
    [HttpGet("by-category/{categoryId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByCategory(Guid categoryId)
    {
        var data = await _service.GetHousesByCategoryIdAsync(categoryId);
        return Ok(data);
    }

    // AZ: Tək ev (hamı)
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetHouseByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    // AZ: Yarat (admin) — şəkillər üçün [FromForm]
    [HttpPost]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Create([FromForm] HousePostDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _service.CreateHouseAsync(dto);

        // AZ: CreateHouseAsync Id qaytarmır, ona görə 204 və ya 200 qaytara bilərik
        // İstəsən servisi Id qaytaracaq hala gətirək. İndi 204 verək:
        return NoContent();
    }

    // AZ: Yenilə (admin) — şəkil + fayllar üçün [FromForm]
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Update(Guid id, [FromForm] HousePutDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _service.UpdateHouseAsync(id, dto);
        return NoContent();
    }

    // AZ: Hard delete (admin)
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteHouseAsync(id);
        return NoContent();
    }

    // AZ: Soft delete (admin)
    [HttpPatch("{id:guid}/soft-delete")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        await _service.SoftDetectedHouseAsync(id); // Qeyd: səndə metod adı belədir
        return NoContent();
    }

    // AZ: Restore (admin)
    [HttpPatch("{id:guid}/restore")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Restore(Guid id)
    {
        await _service.RestoreHouseAsync(id);
        return NoContent();
    }
}
