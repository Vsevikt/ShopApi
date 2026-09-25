using MediatR;
using ShopApplication.DTOs.Category;

namespace ShopApplication.Queries.Category.GetCategoryById;

public record GetCategoryByIdQuery(int Id) : IRequest<CategoryReadDTO?>;