using System.Security.Claims;
using BusinessLogic.DTO.ReviewDTOs;
using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewsController(IReviewService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize] // Yalnız giriş edənlər rəy yaza bilər
    public async Task<IActionResult> Create([FromBody] ReviewPostDTO dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userName = User.Identity?.Name ?? "İstifadəçi";

        if (string.IsNullOrWhiteSpace(userId)) return Unauthorized();

        await _service.CreateAsync(dto, userId, userName);
        return Ok();
    }

    [HttpGet("house/{houseId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByHouse(Guid houseId)
    {
        var data = await _service.GetByHouseIdAsync(houseId);
        return Ok(data);
    }
}