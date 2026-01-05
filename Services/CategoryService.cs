using System;
using GunShop.Data;
using GunShop.DTOs.Categories;
using GunShop.Models;
using GunShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GunShop.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            return await _dbContext.Categories
                .Select(c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null) throw new Exception("Category not found.");

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDto> CreateCategory(CategoryCreateDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task<CategoryDto> UpdateCategory(int id, CategoryUpdateDto dto)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null) throw new Exception("Category not found.");

            category.Name = dto.Name;
            await _dbContext.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null) throw new Exception("Category not found.");

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}

