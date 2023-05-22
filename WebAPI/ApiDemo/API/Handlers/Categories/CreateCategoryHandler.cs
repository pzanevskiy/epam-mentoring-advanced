using API.Requests.Categories;
using ApiDemo.Data;
using ApiDemo.Data.Models;
using MediatR;

namespace API.Handlers.Categories;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryRequest, Category>
{
    private readonly DemoContext _demoContext;

    public CreateCategoryHandler(DemoContext demoContext)
    {
        _demoContext = demoContext;
    }

    public async Task<Category> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var entityEntry = await _demoContext.AddAsync(request.Category, cancellationToken);
        await _demoContext.SaveChangesAsync(cancellationToken);

        return entityEntry.Entity;
    }
}