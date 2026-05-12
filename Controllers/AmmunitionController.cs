using System;
using GunShop.DTOs.Ammunition;
using GunShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GunShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmmunitionController : ControllerBase
    {
        private readonly IAmmunitionService _ammunitionService;

        public AmmunitionController(IAmmunitionService ammunitionService)
        {
            _ammunitionService = ammunitionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ammos = await _ammunitionService.GetAllAsync();
            return Ok(ammos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ammo = await _ammunitionService.GetByIdAsync(id);
            if (ammo == null) return NotFound();
            return Ok(ammo);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] IEnumerable<AmmunitionCreateDto> dtos)
        {
            if (dtos == null || !dtos.Any())
            {
                return BadRequest("მონაცემების სია ცარიელია.");
            }

            var results = await _ammunitionService.CreateRangeAsync(dtos);
            return Ok(results);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AmmunitionUpdateDto dto)
        {
            var updated = await _ammunitionService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ammunitionService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

