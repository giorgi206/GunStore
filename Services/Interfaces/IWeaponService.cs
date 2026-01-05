using System;
using GunShop.DTOs.Weapons;

namespace GunShop.Services.Interfaces
{
	public interface IWeaponService
	{
        Task<List<WeaponDto>> GetAllAsync();
        Task<WeaponDto> CreateAsync(WeaponCreateDto dto);
        Task<WeaponDto> UpdateAsync(int id, WeaponUpdateDto dto);
    }
}

