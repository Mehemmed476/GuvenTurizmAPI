using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatsController : ControllerBase
{
    private readonly IStatsService _service;

    public StatsController(IStatsService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Policy = "RequireAdmin")] // Yalnız Adminlər görsün
    public async Task<IActionResult> GetDashboardStats()
    {
        var data = await _service.GetDashboardStatsAsync();
        return Ok(data);
    }
}