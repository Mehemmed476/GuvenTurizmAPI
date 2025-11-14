using BusinessLogic.DTO.HouseAdvantageDTOs;
using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HouseAdvantagesController : ControllerBase
{
    private readonly IHouseAdvantageService _service;

    public HouseAdvantagesController(IHouseAdvantageService service)
    {
        _service = service;
    }

    // AZ: Bütün üstünlükləri gətir (yalnız admin)
    [HttpGet]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    // AZ: Aktiv (silinməmiş) üstünlükləri gətir (hamı görə bilər)
    [HttpGet("active")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllActive()
    {
        var data = await _service.GetAllActiveAsync();
        return Ok(data);
    }

    // AZ: ID ilə tək üstünlük
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    // AZ: Yeni üstünlük əlavə et
    [HttpPost]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Create([FromBody] HouseAdvantagePostDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _service.CreateAsync(dto);
        var created = await _service.GetByIdAsync(id);
        return CreatedAtAction(nameof(GetById), new { id }, created);
    }

    // AZ: Mövcud üstünlüyü yenilə
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] HouseAdvantagePutDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    // AZ: Üstünlüyü tam sil (hard delete)
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
