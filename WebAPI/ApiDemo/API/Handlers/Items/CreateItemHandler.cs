using API.Requests.Items;
using ApiDemo.Data;
using ApiDemo.Data.Models;
using MediatR;

namespace API.Handlers.Items;

public class CreateItemHandler : IRequestHandler<CreateItemRequest, Item>
{
    private readonly DemoContext _demoContext;

    public CreateItemHandler(DemoContext demoContext)
    {
        _demoContext = demoContext;
    }

    public async Task<Item> Handle(CreateItemRequest request, CancellationToken cancellationToken)
    {
        var entityEntry = await _demoContext.AddAsync(request.Item, cancellationToken);
        await _demoContext.SaveChangesAsync(cancellationToken);

        return entityEntry.Entity;
    }
}