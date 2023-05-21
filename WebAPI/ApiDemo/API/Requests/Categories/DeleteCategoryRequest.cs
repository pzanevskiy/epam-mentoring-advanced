using MediatR;

namespace API.Requests.Categories
{
    public class DeleteCategoryRequest : IRequest
    {
        public int Id { get; set; }
    }
}
