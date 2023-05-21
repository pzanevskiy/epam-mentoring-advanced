using ApiDemo.Data.Models;
using MediatR;

namespace API.Requests.Items;

public class GetFilteredItemsRequest : IRequest<IEnumerable<Item>>
{
    public int CategoryId { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 1;
}