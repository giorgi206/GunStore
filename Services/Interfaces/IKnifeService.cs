using System;
using GunShop.DTOs.Knifes;

namespace GunShop.Services.Interfaces
{
	public interface IKnifeService
	{
        Task<IEnumerable<KnifeDto>> GetAllAsync();
        Task<KnifeDto?> GetByIdAsync(int id);
        Task<KnifeDto> CreateAsync(KnifeCreateDto dto);
        Task<KnifeDto?> UpdateAsync(int id, KnifeUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

