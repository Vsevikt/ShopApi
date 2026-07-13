using ShopApplication.DTOs.Category;

namespace ShopApi.Requests.Categories
{
    public class CategoryCreateRequest : CategoryCreate
    {
        public IFormFile? Image { get; set; }
    }
}
