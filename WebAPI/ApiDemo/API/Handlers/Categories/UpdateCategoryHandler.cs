using API.Requests.Categories;
using ApiDemo.Data;
using ApiDemo.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Categories;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, Category>
{
    private readonly DemoContext _demoContext;

    public UpdateCategoryHandler(DemoContext demoContext)
    {
        _demoContext = demoContext;
    }

    public async Task<Category> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category =
            await _demoContext.FindAsync<Category>(request.Id, cancellationToken);

        if (category != null)
        {
            category.Name = request.EditedCategory.Name;

            _demoContext.Entry(category).State = EntityState.Modified;
            _demoContext.Update(category);
            await _demoContext.SaveChangesAsync(cancellationToken);

            return category;
        }

        return null;
    }
}