using AutoMapper;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApplication.DTOs.Product;
using ShopApplication.Interfaces.Repositories;
using ShopApplication.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services;

public class CategoryService(ICategoryRepository _repository, IMapper _mapper, ICachingService _cacheService) : ICategoryService
{
    public async Task<int?> CreateCategoryAsync(CategoryCreateDTO dto)
    {
        var category = _mapper.Map<Category>(dto);
        return await _repository.AddCategoryAsync(category);
    }

    public async Task<ICollection<CategoryReadDTO>> GetAllCategoriesAsync()
    {
        var cache = await _cacheService.GetAsync<ICollection<CategoryReadDTO>>("categories");
        if (cache == null)
        {
            var categories = await _repository.GetCategoriesAsync();
            cache = _mapper.Map<ICollection<CategoryReadDTO>>(categories);
            await _cacheService.SetAsync("categories", cache, TimeSpan.FromMinutes(5));
           
        }
        return cache;
    }

    public async Task<CategoryReadDTO?> GetCategoryByIdAsync(int id)
    {
        var cache = await _cacheService.GetAsync<CategoryReadDTO>($"categories/{id}");

        if (cache == null)
        {
            var category = await _repository.GetCategoryAsync(id);

            if (category == null)
                return null;

            cache = _mapper.Map<CategoryReadDTO>(category);
            await _cacheService.SetAsync($"categories/{id}", cache, TimeSpan.FromMinutes(5));
        }

        return cache;
    }

    public async Task<ICollection<CategoryReadDTO>> GetCategoriesByParentAsync()
    {
        var categories = await _repository.GetParentCategoriesAsync();
        List<CategoryReadDTO> dtos = null;
        if (categories != null && categories.Count > 0)
            dtos = _mapper.Map<List<CategoryReadDTO>>(categories);
        return dtos;
    }

    public async Task<ICollection<CategoryReadDTO>> GetCategoriesByChildAsync()
    {
        var categories = await _repository.GetChildCategoriesAsync();
        List<CategoryReadDTO> dtos = null;
        if (categories != null && categories.Count > 0)
            dtos = _mapper.Map<List<CategoryReadDTO>>(categories);
        return dtos;
    }

    public async Task<ICollection<CategoryReadDTO>> GetCategoriesByTreeAsync()
    {
        var categories = await _repository.GetTreeCategoriesAsync();
        List<CategoryReadDTO> dtos = null;
        if (categories != null && categories.Count > 0)
            dtos = _mapper.Map<List<CategoryReadDTO>>(categories);
        return dtos;
    }

    public async Task<bool> UpdateCategoryAsync(CategoryUpdateDTO dto)
    {
        var category = await _repository.GetCategoryAsync(dto.Id);
        if (category == null)
            return false;
        _mapper.Map(dto, category);
        category.ParentId = dto.ParentId = dto.ParentId == 0 ? null : dto.ParentId;
        return await _repository.EditCategoryAsync(category);
    }

    public async Task<int?> DeleteCategoryAsync(int id)
    {
        return await _repository.RemoveCategoryAsync(id);
    }
}
