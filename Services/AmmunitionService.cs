using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.DTOs.Ammunition;
using GunShop.Models;
using GunShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Services
{
    public class AmmunitionService : IAmmunitionService
    {
        private readonly ApplicationDbContext _context;

        public AmmunitionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AmmunitionDto>> GetAllAsync()
        {
            return await _context.Ammunitions
                .Include(a => a.Category)
                .Select(a => new AmmunitionDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Manufacturer = a.Manufacturer,
                    Caliber = a.Caliber,
                    Price = a.Price,
                    Stock = a.Stock,
                    ImageUrl = a.ImageUrl,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category.Name
                }).ToListAsync();
        }

        public async Task<AmmunitionDto?> GetByIdAsync(int id)
        {
            var a = await _context.Ammunitions
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (a == null) return null;

            return new AmmunitionDto
            {
                Id = a.Id,
                Name = a.Name,
                Manufacturer = a.Manufacturer,
                Caliber = a.Caliber,
                Price = a.Price,
                Stock = a.Stock,
                ImageUrl = a.ImageUrl,
                CategoryId = a.CategoryId,
                CategoryName = a.Category.Name
            };
        }

        public async Task<AmmunitionDto> CreateAsync(AmmunitionCreateDto dto)
        {
            var ammo = new Ammunition
            {
                Name = dto.Name,
                Manufacturer = dto.Manufacturer,
                Caliber = dto.Caliber,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId
            };

            _context.Ammunitions.Add(ammo);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(ammo.Id);
        }

        public async Task<IEnumerable<AmmunitionDto>> CreateRangeAsync(IEnumerable<AmmunitionCreateDto> dtos)
        {
            var ammunitions = dtos.Select(dto => new Ammunition
            {
                Name = dto.Name,
                Manufacturer = dto.Manufacturer,
                Caliber = dto.Caliber,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId
            }).ToList();

            await _context.Ammunitions.AddRangeAsync(ammunitions);
            await _context.SaveChangesAsync();

            var ids = ammunitions.Select(a => a.Id).ToList();
            return await _context.Ammunitions
                .Include(a => a.Category)
                .Where(a => ids.Contains(a.Id))
                .Select(a => new AmmunitionDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Manufacturer = a.Manufacturer,
                    Caliber = a.Caliber,
                    Price = a.Price,
                    Stock = a.Stock,
                    ImageUrl = a.ImageUrl,
                    CategoryId = a.CategoryId,
                    CategoryName = a.Category.Name
                }).ToListAsync();
        }

        public async Task<AmmunitionDto?> UpdateAsync(int id, AmmunitionUpdateDto dto)
        {
            var ammo = await _context.Ammunitions.FindAsync(id);
            if (ammo == null) return null;

            ammo.Name = dto.Name;
            ammo.Manufacturer = dto.Manufacturer;
            ammo.Caliber = dto.Caliber;
            ammo.Price = dto.Price;
            ammo.Stock = dto.Stock;
            ammo.ImageUrl = dto.ImageUrl;
            ammo.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ammo = await _context.Ammunitions.FindAsync(id);
            if (ammo == null) return false;

            _context.Ammunitions.Remove(ammo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}