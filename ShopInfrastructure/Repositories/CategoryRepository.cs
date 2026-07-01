using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
