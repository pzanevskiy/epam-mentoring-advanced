using API.Requests.Items;
using ApiDemo.Data;
using ApiDemo.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Items;

public class UpdateItemHandler : IRequestHandler<UpdateItemRequest, Item>
{
    private readonly DemoContext _demoContext;

    public UpdateItemHandler(DemoContext demoContext)
    {
        _demoContext = demoContext;
    }

    public async Task<Item> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
    {
        var item =
            await _demoContext.FindAsync<Item>(request.Id, cancellationToken);

        if (item != null)
        {
            item.Name = request.EditedItem.Name;
            item.Description = request.EditedItem.Description;
            item.Price = request.EditedItem.Price;
            item.CategoryId = request.EditedItem.CategoryId;

            _demoContext.Entry(item).State = EntityState.Modified;
            _demoContext.Update(item);
            await _demoContext.SaveChangesAsync(cancellationToken);

            return item;
        }

        return null;
    }
}