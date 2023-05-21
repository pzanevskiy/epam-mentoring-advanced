using MediatR;

namespace API.Requests.Items
{
    public class DeleteItemRequest : IRequest
    {
        public int Id { get; set; }
    }
}
