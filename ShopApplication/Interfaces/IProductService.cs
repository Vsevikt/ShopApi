using ShopApplication.DTOs.Product;
using ShopDomain.Models;

namespace ShopApplication.Interfaces
{
    public interface IProductService
    {
        List<ProductReadDto> GetAllProducts();
        ProductReadDto? GetProduct(int id);
        ProductReadDto AddProduct(ProductCreateDto product);
        ProductReadDto? UpdateProduct(int id, ProductUpdateDto dto);
        void DeleteProduct(int id);
        Product? SearchProduct(string? title);
    }
}
