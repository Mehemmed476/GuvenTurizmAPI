using System.Security.Claims;
using BusinessLogic.DTO.BookingDTOs;
using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _service;

    public BookingsController(IBookingService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("active")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllActive()
    {
        var data = await _service.GetAllActiveAsync();
        return Ok(data);
    }

    [HttpGet("house/{houseId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByHouseId(Guid houseId)
    {
        var data = await _service.GetByHouseIdAsync(houseId);
        return Ok(data);
    }

    [HttpGet("user/{userId}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> GetByUserId(string userId)
    {
        var data = await _service.GetByUserIdAsync(userId);
        return Ok(data);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId)) return Unauthorized();

        var data = await _service.GetByUserIdAsync(userId);
        return Ok(data);
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] BookingPostDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var id = await _service.CreateAsync(dto);
        var created = await _service.GetByIdAsync(id);
        return CreatedAtAction(nameof(GetById), new { id }, created);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, [FromBody] BookingPutDTO dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await _service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPatch("{id:guid}/soft-delete")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> SoftDelete(Guid id)
    {
        await _service.SoftDeleteAsync(id);
        return NoContent();
    }

    [HttpPatch("{id:guid}/restore")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> Restore(Guid id)
    {
        await _service.RestoreAsync(id);
        return NoContent();
    }
}
