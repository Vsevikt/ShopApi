using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Interfaces.Repository;
using ShopDomain.Models;
using ShopInfrastructure.Data;

namespace ShopInfrastructure.Repositories
{
    public class CategoryRepository(ShopDbContext _context) : ICategoryRepository
    {
        public async Task<int?> AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> EditCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int?> RemoveCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return null;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}
