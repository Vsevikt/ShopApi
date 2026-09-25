using AutoMapper;
using MediatR;
using ShopApplication.DTOs.Category;
using ShopApplication.Interfaces.Repositories;

namespace ShopApplication.Queries.Category.GetCategoryBySlug;

public class GetCategoryBySlugHandler(ICategoryRepository repository, IMapper _mapper) : IRequestHandler<GetCategoryBySlugQuery, CategoryReadDTO?>
{
    public async Task<CategoryReadDTO?> Handle(GetCategoryBySlugQuery request, CancellationToken cancellationToken)
    {
        var category = await repository.GetBySlugAsync(request.slug);
        if (category == null)
            return null;
        return _mapper.Map<CategoryReadDTO>(category);
    }
}