using AutoMapper;
using MediatR;
using ShopApplication.DTOs.Product;
using ShopApplication.Interfaces.Repositories;
using ShopApplication.Queries.Product;

namespace ShopApplication.Queries.GetProductById;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductReadDTO?>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductByIdHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ProductReadDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProductAsync(request.id);
        if (product == null)
            return null;
        return _mapper.Map<ProductReadDTO>(product);
    }
}