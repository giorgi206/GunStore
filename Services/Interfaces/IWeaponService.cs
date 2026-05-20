using System;
using GunShop.DTOs.Weapons;
namespace GunShop.Services.Interfaces
{
    public interface IWeaponService
    {
        Task<List<WeaponDto>> GetAllAsync(int? categoryId = null);
        Task<WeaponDto> GetByIdAsync(int id);
        Task<WeaponDto> CreateAsync(WeaponCreateDto dto);
        Task<WeaponDto> UpdateAsync(int id, WeaponUpdateDto dto);
    }
}