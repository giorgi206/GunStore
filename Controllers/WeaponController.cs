using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.DTOs.Weapons;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GunShop.Controllers
{
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        public WeaponController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<WeaponDto>>> GetWeaponList()
        {
            var weapons = await _dbContext.Weapons.ToListAsync();
            if(weapons == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(weapons);
            }
        }
    }
}

