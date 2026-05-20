using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.DTOs.Knifes;
using GunShop.Services;
using GunShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GunShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KnifeController : ControllerBase
    {
        private readonly IKnifeService _knifeService;

        public KnifeController(IKnifeService knifeService)
        {
            _knifeService = knifeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var knives = await _knifeService.GetAllAsync();
            return Ok(knives);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var knife = await _knifeService.GetByIdAsync(id);
            if (knife == null) return NotFound();
            return Ok(knife);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] KnifeCreateDto dto)
        {
            var created = await _knifeService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // ეს არის ის მეთოდი, რომელიც დაგეხმარებათ მასიურად ატვირთვაში
        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] IEnumerable<KnifeCreateDto> dtos)
        {
            if (dtos == null || !dtos.Any())
            {
                return BadRequest("მონაცემების მასივი ცარიელია.");
            }

            var results = await _knifeService.CreateRangeAsync(dtos);
            return Ok(results);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] KnifeUpdateDto dto)
        {
            var updated = await _knifeService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _knifeService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}