using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApplication.DTOs.Product;
using ShopApplication.Interfaces;
using ShopApplication.Interfaces.Repositories;
using ShopApplication.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services
{
    public class ProductService(IProductRepository _repository, IImageService _imageService, IMapper _mapper, ICachingService _cacheService) : IProductService
    {
        public async Task<int?> CreateProductAsync(ProductCreateDTO dto)
        {
            var product = _mapper.Map<Product>(dto);
            return await _repository.AddProductAsync(product);
        }

        public async Task<ICollection<ProductReadDTO>> GetAllProductsAsync()
        {
            var cache = await _cacheService.GetAsync<ICollection<ProductReadDTO>>("products");

            if (cache == null)
            {
                var products = await _repository.GetProductsAsync();
                cache = _mapper.Map<ICollection<ProductReadDTO>>(products);
                await _cacheService.SetAsync("products", cache, TimeSpan.FromMinutes(5));
            }

            return cache;
        }

        public async Task<ProductReadDTO?> GetProductByIdAsync(int id)
        {
            var cache = await _cacheService.GetAsync<ProductReadDTO>($"products/{id}");

            if (cache == null)
            {
                var product = await _repository.GetProductAsync(id);

                if (product == null)
                    return null;

                cache = _mapper.Map<ProductReadDTO>(product);
                await _cacheService.SetAsync($"products/{id}", cache, TimeSpan.FromMinutes(5));
            }

            return cache;
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateDTO dto)
        {
            var product = await _repository.GetProductAsync(dto.Id);

            if (product == null)
                return false;

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.StockQty = dto.StockQty;
            _ = product.CategoryId == 0 ? null : dto.CategoryId;

            if (dto.ImageUrls?.Any() == true)
            {
                var oldImages = product.Images.ToList();

                foreach (var image in oldImages)
                {
                    _imageService.DeleteFile(image.Url, "products");
                }

                _repository.RemoveImages(oldImages);

                product.Images = dto.ImageUrls
                    .Select((url, index) => new ProductImage
                    {
                        Url = string.IsNullOrEmpty(url) ? "default-image.png" : url,
                        ProductId = product.Id,
                        IsPrimary = index == 0
                    })
                    .ToList();
            }

            return await _repository.EditProductAsync(product);
        }

        public async Task<int?> DeleteProductAsync(int id)
        {
            return await _repository.RemoveProductAsync(id);
        }

        public async Task<ICollection<ProductReadDTO>> SearchProductAsync(string name)
        {
            var products = await _repository.SearchProductByNameAsync(name);
            return _mapper.Map<List<ProductReadDTO>>(products);
        }
    }
}
