using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.DTOs.Knifes;
using GunShop.Models;
using GunShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Services
{
    public class KnifeService : IKnifeService
    {
        private readonly ApplicationDbContext _context;

        public KnifeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<KnifeDto>> GetAllAsync()
        {
            return await _context.Knives
                .Include(k => k.Category)
                .Select(k => MapToDto(k))
                .ToListAsync();
        }

        public async Task<KnifeDto?> GetByIdAsync(int id)
        {
            var k = await _context.Knives
                .Include(k => k.Category)
                .FirstOrDefaultAsync(k => k.Id == id);

            return k == null ? null : MapToDto(k);
        }

        public async Task<KnifeDto> CreateAsync(KnifeCreateDto dto)
        {
            var knife = new Knife
            {
                Name = dto.Name,
                BladeType = dto.BladeType,         // ✅ გასწორებული
                BladeMaterial = dto.BladeMaterial,
                BladeLength = dto.BladeLength,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId
            };

            _context.Knives.Add(knife);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(knife.Id);
        }

        public async Task<IEnumerable<KnifeDto>> CreateRangeAsync(IEnumerable<KnifeCreateDto> dtos)
        {
            var knives = dtos.Select(dto => new Knife
            {
                Name = dto.Name,
                BladeType = dto.BladeType,         // ✅ გასწორებული
                BladeMaterial = dto.BladeMaterial,
                BladeLength = dto.BladeLength,
                Price = dto.Price,
                Stock = dto.Stock,
                ImageUrl = dto.ImageUrl,
                CategoryId = dto.CategoryId
            }).ToList();

            await _context.Knives.AddRangeAsync(knives);
            await _context.SaveChangesAsync();

            var ids = knives.Select(k => k.Id).ToList();
            return await _context.Knives
                .Include(k => k.Category)
                .Where(k => ids.Contains(k.Id))
                .Select(k => MapToDto(k))
                .ToListAsync();
        }

        public async Task<KnifeDto?> UpdateAsync(int id, KnifeUpdateDto dto)
        {
            var knife = await _context.Knives.FindAsync(id);
            if (knife == null) return null;

            knife.Name = dto.Name;
            knife.BladeType = dto.BladeType;       // ✅ გასწორებული
            knife.BladeMaterial = dto.BladeMaterial;
            knife.BladeLength = dto.BladeLength;
            knife.Price = dto.Price;
            knife.Stock = dto.Stock;
            knife.ImageUrl = dto.ImageUrl;
            knife.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var knife = await _context.Knives.FindAsync(id);
            if (knife == null) return false;

            _context.Knives.Remove(knife);
            await _context.SaveChangesAsync();
            return true;
        }

        private static KnifeDto MapToDto(Knife k)
        {
            return new KnifeDto
            {
                Id = k.Id,
                Name = k.Name,
                BladeType = k.BladeType,           // ✅ გასწორებული
                BladeMaterial = k.BladeMaterial,
                BladeLength = k.BladeLength,
                Price = k.Price,
                Stock = k.Stock,
                ImageUrl = k.ImageUrl,
                CategoryId = k.CategoryId,
                CategoryName = k.Category?.Name ?? "Unknown"
            };
        }
    }
}