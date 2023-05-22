using ApiDemo.Data.Models;
using MediatR;

namespace API.Requests.Items;

public class UpdateItemRequest : IRequest<Item>
{
    public int Id { get; set; }

    public Item EditedItem { get; set; }
}