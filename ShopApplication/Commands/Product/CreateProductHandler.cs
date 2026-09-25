using MediatR;
using ShopApplication.Commands.Product;
using ShopApplication.Interfaces.Repositories;
using ShopDomain.Models;

namespace ShopApplication.Products.Commands.CreateProduct;

public class CreateProductHandler(IProductRepository repository) : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = command.Name,
            Price = command.Price,
            StockQty = command.StockQty,
            CategoryId = command.CategoryId
        };

        foreach (var imageUrl in command.ImageUrls)
        {
            product.Images.Add(new ProductImage
            {
                Url = imageUrl
            });
        }

        await repository.AddProductAsync(product);

        return product.Id;
    }
}