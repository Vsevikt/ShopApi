using ShopApplication.DTOs.Product;

namespace ShopApi.Requests.Products
{
    public class ProductUpdateRequest : ProductUpdate
    {
        public List<IFormFile> Images { get; set; } = new();
    }
}
