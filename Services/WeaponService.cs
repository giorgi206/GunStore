using System;
using GunShop.Data;
using GunShop.DTOs.Weapons;
using GunShop.Models;
using GunShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Services
{
	public class WeaponService : IWeaponService
	{
        private readonly ApplicationDbContext _context;

        public WeaponService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WeaponDto>> GetAllAsync()
        {
            return await _context.Weapons
                .Select(w => new WeaponDto
                {
                    Id = w.Id,
                    Name = w.Name,
                    Manufacturer = w.Manufacturer,
                    Caliber = w.Caliber,
                    Price = w.Price,
                    Stock = w.Stock,
                    ImageUrl = w.ImageUrl,
                    CategoryId = w.CategoryId
                })
                .ToListAsync();
        }

        public async Task<WeaponDto> CreateAsync(WeaponCreateDto dto)
        {
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == dto.CategoryId);

            if (!categoryExists)
            {
                throw new Exception($"Category with Id {dto.CategoryId} does not exist.");
            }
            var weapon = new Weapon
            {
                Name = dto.Name,
                Manufacturer = dto.Manufacturer,
                Caliber = dto.Caliber,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId
            };

            _context.Weapons.Add(weapon);
            await _context.SaveChangesAsync();

            return new WeaponDto
            {
                Id = weapon.Id,
                Name = weapon.Name,
                Manufacturer = weapon.Manufacturer,
                Caliber = weapon.Caliber,
                Price = weapon.Price,
                Stock = weapon.Stock,
                ImageUrl = weapon.ImageUrl,
                CategoryId = weapon.CategoryId
            };
        }

        public async Task<WeaponDto> UpdateAsync(int id, WeaponUpdateDto dto)
        {
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                throw new Exception("Weapon not found");
            }
            weapon.Name = dto.Name;
            weapon.Manufacturer = dto.Manufacturer;
            weapon.Caliber = dto.Caliber;
            weapon.Price = dto.Price;
            weapon.Stock = dto.Stock;
            weapon.ImageUrl = dto.ImageUrl;
            weapon.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return new WeaponDto
            {
                Id = weapon.Id,
                Name = weapon.Name,
                Manufacturer = weapon.Manufacturer,
                Caliber = weapon.Caliber,
                Price = weapon.Price,
                Stock = weapon.Stock,
                ImageUrl = weapon.ImageUrl,
                CategoryId = weapon.CategoryId
            };
        }
    }
}

