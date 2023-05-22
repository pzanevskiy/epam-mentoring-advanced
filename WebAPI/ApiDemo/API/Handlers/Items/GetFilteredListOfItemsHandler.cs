using API.Requests.Items;
using ApiDemo.Data;
using ApiDemo.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace API.Handlers.Items
{
    public class GetFilteredListOfItemsHandler : IRequestHandler<GetFilteredItemsRequest, IEnumerable<Item>>
    {
        private readonly DemoContext _demoContext;

        public GetFilteredListOfItemsHandler(DemoContext demoContext)
        {
            _demoContext = demoContext;
        }

        public async Task<IEnumerable<Item>> Handle(GetFilteredItemsRequest request, CancellationToken cancellationToken)
        {
            if (request.CategoryId == 0)
            {
                return await _demoContext.Items
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);
            }

            return await _demoContext.Items
                 .Where(x => x.CategoryId == request.CategoryId)
                 .Skip((request.Page - 1) * request.PageSize)
                 .Take(request.PageSize)
                 .ToListAsync(cancellationToken);
        }
    }
}
