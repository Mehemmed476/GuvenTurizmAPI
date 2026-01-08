using BusinessLogic.DTO.TourDTOs;
using BusinessLogic.Service.Abstractions;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToursController : ControllerBase
    {
        private readonly ITourService _tourService;

        public ToursController(ITourService tourService)
        {
            _tourService = tourService;
        }

        // --- PUBLIC ENDPOINTS (Herkese Açık) ---

        [HttpGet]
        public async Task<IActionResult> GetAllActive([FromQuery] int page = 1, [FromQuery] int size = 9)
        {
            var result = await _tourService.GetAllActiveToursAsync(page, size);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _tourService.GetTourByIdAsync(id);
            if (result == null) return NotFound("Tur tapılmadı.");
            return Ok(result);
        }

        [HttpGet("location/{location}")]
        public async Task<IActionResult> GetByLocation(string location)
        {
            var result = await _tourService.GetToursByLocationAsync(location);
            return Ok(result);
        }

        // --- ADMIN ENDPOINTS (Sadece Admin Yetkili) ---

        [Authorize(Roles = "Admin")]
        [HttpGet("admin/all")]
        public async Task<IActionResult> GetAllForAdmin()
        {
            // Silinenler dahil tüm listeyi getirir
            var result = await _tourService.GetAllToursAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TourPostDTO dto)
        {
            // [FromForm] kullanıyoruz çünkü dosya (resim) yüklemesi var
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _tourService.CreateTourAsync(dto);
            return StatusCode(201, "Tur uğurla yaradıldı.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromForm] TourPutDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Id kontrolü (DTO içinde Id olmadığı için URL'den geleni kullanıyoruz)
            await _tourService.UpdateTourAsync(id, dto);
            return Ok("Tur uğurla yeniləndi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // Tamamen veritabanından siler (Hard Delete)
            await _tourService.DeleteTourAsync(id);
            return Ok("Tur tamamilə silindi.");
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/soft-delete")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            // Çöp kutusuna atar
            await _tourService.SoftDeleteTourAsync(id);
            return Ok("Tur zibil qutusuna atıldı (Soft Deleted).");
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            // Geri yükler
            await _tourService.RestoreTourAsync(id);
            return Ok("Tur geri yükləndi.");
        }
    }
}