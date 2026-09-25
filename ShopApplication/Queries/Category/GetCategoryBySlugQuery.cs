using MediatR;
using ShopApplication.DTOs.Category;

namespace ShopApplication.Queries.Category.GetCategoryBySlug;

public record GetCategoryBySlugQuery(string slug) : IRequest<CategoryReadDTO?>;