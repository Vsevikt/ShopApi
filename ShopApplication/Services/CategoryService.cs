using AutoMapper;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.CategoryDTOs;
using ShopApplication.Interfaces.Repository;
using ShopApplication.Interfaces.Services;
using ShopDomain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApplication.Services;

public class CategoryService(ICategoryRepository _repository, IMapper _mapper) : ICategoryService
{
    public async Task<int?> CreateCategoryAsync(CategoryCreate dto)
    {
        var category = _mapper.Map<Category>(dto);
        return await _repository.AddCategoryAsync(category);
    }

    public async Task<ICollection<CategoryRead>> GetAllCategoriesAsync()
    {
        var categories = await _repository.GetCategoriesAsync();
        List<CategoryRead> dtos = null;
        if (categories != null && categories.Count > 0)
            dtos = _mapper.Map<List<CategoryRead>>(categories);
        return dtos;
    }

    public async Task<CategoryRead?> GetCategoryByIdAsync(int id)
    {
        CategoryRead? dto = null;
        var category = await _repository.GetCategoryAsync(id);
        if (category != null)
            dto = _mapper.Map<CategoryRead>(category);
        return dto;
    }

    public async Task<ICollection<CategoryRead>> GetCategoriesByParentAsync()
    {
        var categories = await _repository.GetParentCategoriesAsync();
        List<CategoryRead> dtos = null;
        if (categories != null && categories.Count > 0)
            dtos = _mapper.Map<List<CategoryRead>>(categories);
        return dtos;
    }

    public async Task<ICollection<CategoryRead>> GetCategoriesByChildAsync()
    {
        var categories = await _repository.GetChildCategoriesAsync();
        List<CategoryRead> dtos = null;
        if (categories != null && categories.Count > 0)
            dtos = _mapper.Map<List<CategoryRead>>(categories);
        return dtos;
    }

    public async Task<ICollection<CategoryRead>> GetCategoriesByTreeAsync()
    {
        var categories = await _repository.GetTreeCategoriesAsync();
        List<CategoryRead> dtos = null;
        if (categories != null && categories.Count > 0)
            dtos = _mapper.Map<List<CategoryRead>>(categories);
        return dtos;
    }

    public async Task<bool> UpdateCategoryAsync(CategoryUpdate dto)
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
