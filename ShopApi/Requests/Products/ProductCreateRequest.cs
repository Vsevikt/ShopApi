using ShopApplication.DTOs.Product;

namespace ShopApi.Requests.Products
{
    public class ProductCreateRequest : ProductCreate
    {
        public List<IFormFile?> Images { get; set; }
    }
}
