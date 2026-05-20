using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.DTOs.Weapons;
using GunShop.Models;
using GunShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GunShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWeaponList([FromQuery] int? categoryId)
        {
            var weapons = await _weaponService.GetAllAsync(categoryId);
            return Ok(weapons);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeaponById(int id)
        {
            var weapon = await _weaponService.GetByIdAsync(id);
            return Ok(weapon);
        }
        [HttpPost]
        public async Task<IActionResult> CreateWeaponProduct(WeaponCreateDto dto)
        {
            var weapon = await _weaponService.CreateAsync(dto);
            return Ok(weapon);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeapon(int id, WeaponUpdateDto dto)
        {
            var weapon = await _weaponService.UpdateAsync(id, dto);
            return Ok(weapon);
        }
    }
}

