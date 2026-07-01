using ShopApplication.DTOs.Category;
using ShopApplication.Interfaces.Repository;
using ShopApplication.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services;

public class CategoryService(ICategoryRepository _repository): ICategoryService
{
    public async Task<int>? ICategoryService.CreateCategoryAsync(CategoryCreateDTO dto)
    {
        return await _repository.AddCategoryAsync(new Category()
        {
            Name = dto.Name,
            Slug = dto.Slug,
            Url = dto.Url,
            ParentId = dto.ParentId,
        });
    }
}
