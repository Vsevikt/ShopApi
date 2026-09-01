using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApplication.DTOs.Product;
using ShopApplication.DTOs.ProductDTOs;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Interfaces.Services
{
    public interface IProductService
    {
        Task<int?> CreateProductAsync(ProductCreateDTO dto);
        Task<ICollection<ProductReadDTO>> GetAllProductsAsync();
        Task<ProductReadDTO?> GetProductByIdAsync(int id);
        Task<bool> UpdateProductAsync(ProductUpdateDTO dto);
        Task<int?> DeleteProductAsync(int id);
        Task<ICollection<ProductReadDTO>> SearchProductAsync(string name);
    }
}
