using MediatR;
using ShopApplication.Commands.Category;
using ShopApplication.Interfaces.Repositories;
using ShopDomain.Models;

public class CreateCategoryHandler(ICategoryRepository repository) : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = command.Name,
            Slug = command.Slug,
            Url = command.Url,
            ParentId = command.ParentId
        };
        await repository.AddCategoryAsync(category);
        return category.Id;
    }
}