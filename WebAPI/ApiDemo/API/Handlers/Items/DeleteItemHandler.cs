using API.Requests.Items;
using ApiDemo.Data;
using ApiDemo.Data.Models;
using MediatR;

namespace API.Handlers.Items
{
    public class DeleteItemHandler : IRequestHandler<DeleteItemRequest>
    {
        private readonly DemoContext _demoContext;

        public DeleteItemHandler(DemoContext demoContext)
        {
            _demoContext = demoContext;
        }

        public async Task Handle(DeleteItemRequest request, CancellationToken cancellationToken)
        {
            var item =
                await _demoContext.FindAsync<Item>(request.Id, cancellationToken);

            if (item != null)
            {
                _demoContext.Remove(item);
                await _demoContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
