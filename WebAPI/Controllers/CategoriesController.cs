using BusinessLogic.DTO.CategoryDTOs;
using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    // AZ: Bütün kateqoriyalar (admin)
    [HttpGet]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    // AZ: Yalnız aktiv kateqoriyalar (hamı üçün açıq)
    [HttpGet("active")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllActive()
    {
        var data = await _service.GetAllActiveAsync();
        return Ok(data);
    }

    // AZ: Id üzrə tək kateqoriya
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    // AZ: Yarat (DTO-da HouseId gələrsə, evə bağlanacaq)
    [HttpPost]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Create([FromBody] CategoryPostDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var id = await _service.CreateAsync(dto);
        var created = await _service.GetByIdAsync(id);
        return CreatedAtAction(nameof(GetById), new { id }, created);
    }

    // AZ: Yenilə (DTO-da HouseId gələrsə, ev yenidən bağlanacaq)
    [HttpPut("{id:guid}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Update(Guid id, [FromBody] CategoryPutDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    // AZ: Tam sil (əgər bağlı ev varsa, service Exception atır)
    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    // AZ: Soft delete
    [HttpPatch("{id:guid}/soft-delete")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        await _service.SoftDeleteAsync(id);
        return NoContent();
    }

    // AZ: Bərpa
    [HttpPatch("{id:guid}/restore")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Restore(Guid id)
    {
        await _service.RestoreAsync(id);
        return NoContent();
    }
}
