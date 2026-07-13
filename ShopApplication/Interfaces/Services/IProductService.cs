using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApplication.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface IProductService
    {
        Task<int?> CreateProductAsync(ProductCreate dto);
        Task<ICollection<ProductRead>> GetAllProductsAsync();
        Task<ProductRead?> GetProductByIdAsync(int id);
        Task<bool> UpdateProductAsync(ProductUpdate dto);
        Task<int?> DeleteProductAsync(int id);
    }
}
