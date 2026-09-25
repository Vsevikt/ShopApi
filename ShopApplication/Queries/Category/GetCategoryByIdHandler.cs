using AutoMapper;
using MediatR;
using ShopApplication.DTOs.Category;
using ShopApplication.DTOs.Product;
using ShopApplication.Interfaces;
using ShopApplication.Interfaces.Repositories;

namespace ShopApplication.Queries.Category.GetCategoryById;

public class GetCategoryByIdHandler(ICategoryRepository repository, IMapper _mapper) : IRequestHandler<GetCategoryByIdQuery, CategoryReadDTO?>
{
    public async Task<CategoryReadDTO?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await repository.GetCategoryAsync(request.Id);
        if (category == null)
            return null;
        return _mapper.Map<CategoryReadDTO>(category);
    }
}