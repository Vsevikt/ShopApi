using MediatR;
using ShopApplication.Interfaces.Repositories;

namespace ShopApplication.Commands.Product;

public class DeleteProductByIdHandler(IProductRepository _repository) : IRequestHandler<DeleteProductByIdCommand, int>
{
    public async Task<int> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
    {
        var product = await _repository.GetProductAsync(command.id);
        if (product == null)
            return 0;
        product.IsActive = false;
        await _repository.EditProductAsync(product);
        return product.Id;
    }
}