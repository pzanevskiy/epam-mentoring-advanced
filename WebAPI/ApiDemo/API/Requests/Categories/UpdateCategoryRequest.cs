using ApiDemo.Data.Models;
using MediatR;

namespace API.Requests.Categories;

public class UpdateCategoryRequest : IRequest<Category>
{
    public int Id { get; set; }

    public Category EditedCategory { get; set; }
}