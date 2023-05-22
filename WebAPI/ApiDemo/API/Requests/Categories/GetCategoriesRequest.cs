using ApiDemo.Data.Models;
using MediatR;

namespace API.Requests.Categories
{
    public class GetCategoriesRequest : IRequest<IEnumerable<Category>>
    {
    }
}
