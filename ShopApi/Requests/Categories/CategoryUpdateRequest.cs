using ShopApplication.DTOs.CategoryDTOs;

namespace ShopApi.Requests.Categories
{
    public class CategoryUpdateRequest : CategoryUpdate
    {
        public IFormFile? Image { get; set; }
    }
}
