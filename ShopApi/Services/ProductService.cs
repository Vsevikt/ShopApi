//using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.DTOs.Product;
using ShopApi.Interfaces;
using ShopDomain.Models;
using ShopInfrastructure.Data;

namespace ShopApi.Services
{
    public class ProductService : IProductService
    {
        private List<Product> _products = new();

        private readonly ShopDbContext _context;

        public ProductService()
        {
            //_products.Add(new Product()
            //{
            //    Id = 1,
            //    Name = "Phone 1",
            //    Price = 1000
            //});
            //_products.Add(new Product()
            //{
            //    Id = 2,
            //    Name = "Phone 2",
            //    Price = 2000
            //});
            //_products.Add(new Product()
            //{
            //    Id = 3,
            //    Name = "Phone 3",
            //    Price = 3000
            //});
            //_products.Add(new Product()
            //{
            //    Id = 4,
            //    Name = "Phone 4",
            //    Price = 4000
            //});
        }

        public ProductService(ShopDbContext context)
        {
            _context = context;
        }

        public ProductReadDTO? GetProduct(int id)
        {
            var product = _context.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            return new ProductReadDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
            };
        }

        public ProductReadDTO AddProduct(ProductCreateDTO dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId
            };

            var categoryName = _context.Categories.FirstOrDefault(p => p.Id == dto.CategoryId)?.Name;

            _context.Products.Add(product);
            _context.SaveChanges();

            return new ProductReadDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = categoryName
            };
        }

        public List<ProductReadDTO> GetAllProducts()
        {
            var products = _context.Products
                .Select(p => new ProductReadDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category.Name
                })
                .ToList();

            return products;
        }

        public ProductReadDTO? UpdateProduct(int id, ProductUpdateDTO dto)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;

            _context.Products.Update(product);
            _context.SaveChanges();

            return new ProductReadDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category?.Name
            };
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product? SearchProduct(string? title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return null;

            return _products.FirstOrDefault(p => p.Name != null && p.Name.Equals(title, StringComparison.OrdinalIgnoreCase));
        }
    }
}
