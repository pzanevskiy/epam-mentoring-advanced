using ApiDemo.Data.Models;
using MediatR;

namespace API.Requests.Categories;

public class CreateCategoryRequest : IRequest<Category>
{
    public Category Category { get; set; }
}