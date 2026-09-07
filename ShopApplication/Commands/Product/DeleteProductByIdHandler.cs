using MediatR;
using ShopApplication.Interfaces.Repositories;

namespace ShopApplication.Commands.Product;

public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, int>
{
    private readonly IProductRepository _repository;

    public DeleteProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProductAsync(request.id);
        if (product == null)
            return 0;
        product.IsActive = false;
        await _repository.EditProductAsync(product);
        return product.Id;
    }
}