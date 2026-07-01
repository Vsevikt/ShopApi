using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<int?> AddCategoryAsync(Category category);
    }
}
