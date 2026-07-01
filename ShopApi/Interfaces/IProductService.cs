using ShopApplication.DTOs.Product;
using ShopDomain.Models;

namespace ShopApi.Interfaces
{
    public interface IProductService
    {
        List<ProductReadDTO> GetAllProducts();
        ProductReadDTO? GetProduct(int id);
        ProductReadDTO AddProduct(ProductCreateDTO product);
        ProductReadDTO? UpdateProduct(int id, ProductUpdateDTO dto);
        void DeleteProduct(int id);
        Product? SearchProduct(string? title);
    }
}
