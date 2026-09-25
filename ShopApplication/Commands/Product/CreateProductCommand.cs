using MediatR;

namespace ShopApplication.Commands.Product;

public record CreateProductCommand(string Name, decimal Price, int StockQty, List<string> ImageUrls, int? CategoryId) : IRequest<int>;