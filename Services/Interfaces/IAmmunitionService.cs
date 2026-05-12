using System.Collections.Generic;
using System.Threading.Tasks;
using GunShop.DTOs.Ammunition;

namespace GunShop.Services.Interfaces
{
    public interface IAmmunitionService
    {
        Task<IEnumerable<AmmunitionDto>> GetAllAsync();
        Task<AmmunitionDto?> GetByIdAsync(int id);
        Task<AmmunitionDto> CreateAsync(AmmunitionCreateDto dto);

        // ეს ხაზი აკლია თქვენს ინტერფეისს და სწორედ ამას ითხოვს შეცდომის შეტყობინება:
        Task<IEnumerable<AmmunitionDto>> CreateRangeAsync(IEnumerable<AmmunitionCreateDto> dtos);

        Task<AmmunitionDto?> UpdateAsync(int id, AmmunitionUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}