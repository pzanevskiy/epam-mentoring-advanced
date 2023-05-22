using ApiDemo.Data.Models;
using MediatR;

namespace API.Requests.Items;

public class CreateItemRequest : IRequest<Item>
{
    public Item Item { get; set; }
}