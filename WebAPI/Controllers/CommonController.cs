using BusinessLogic.DTO.CommonDTOs;
using BusinessLogic.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommonController : ControllerBase
{
    private readonly ICommonService _service;

    public CommonController(ICommonService service)
    {
        _service = service;
    }

    [HttpGet("faqs")]
    public async Task<IActionResult> GetFAQs()
    {
        return Ok(await _service.GetFAQsAsync());
    }

    [HttpGet("settings")]
    public async Task<IActionResult> GetSettings()
    {
        return Ok(await _service.GetSettingsAsync());
    }
    
    [HttpPost("faqs")]
    [Authorize(Policy = "RequireAdmin")] // Yalnız Admin
    public async Task<IActionResult> CreateFAQ([FromBody] FAQPostDTO dto)
    {
        await _service.CreateFAQAsync(dto);
        return Ok();
    }

    [HttpPut("faqs")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> UpdateFAQ([FromBody] FAQPutDTO dto)
    {
        await _service.UpdateFAQAsync(dto);
        return Ok();
    }

    [HttpDelete("faqs/{id:guid}")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> DeleteFAQ(Guid id)
    {
        await _service.DeleteFAQAsync(id);
        return NoContent();
    }
    
    [HttpPut("settings")]
    [Authorize(Policy = "RequireAdmin")]
    public async Task<IActionResult> UpdateSetting([FromBody] SettingDTO dto)
    {
        await _service.UpdateSettingAsync(dto.Key, dto.Value);
        return Ok();
    }
}