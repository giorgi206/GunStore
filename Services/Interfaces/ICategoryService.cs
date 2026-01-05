using System;
using GunShop.DTOs.Categories;

namespace GunShop.Services.Interfaces
{
	public interface ICategoryService
	{
        Task<List<CategoryDto>> GetAllCategories();
        Task<CategoryDto> GetCategoryById(int id);
        Task<CategoryDto> CreateCategory(CategoryCreateDto dto);
        Task<CategoryDto> UpdateCategory(int id, CategoryUpdateDto dto);
        Task DeleteCategory(int id);
    }
}

