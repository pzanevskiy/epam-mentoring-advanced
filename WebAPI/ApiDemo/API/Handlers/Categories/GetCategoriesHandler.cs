using API.Requests.Categories;
using ApiDemo.Data;
using ApiDemo.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Categories;

public class GetCategoriesHandler : IRequestHandler<GetCategoriesRequest, IEnumerable<Category>>
{
    private readonly DemoContext _demoContext;

    public GetCategoriesHandler(DemoContext demoContext)
    {
        _demoContext = demoContext;
    }

    public async Task<IEnumerable<Category>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var categories = await _demoContext.Categories.ToListAsync(cancellationToken);

        return categories;
    }
}