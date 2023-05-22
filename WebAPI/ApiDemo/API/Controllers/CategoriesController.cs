using API.Models;
using API.Requests.Categories;
using ApiDemo.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var categories = await _mediator.Send(new GetCategoriesRequest());

        return categories.Any() ? Ok(categories) : NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryModel model)
    {
        var request = new CreateCategoryRequest
        {
            Category = new Category
            {
                Name = model.Name
            }
        };

        var newCategory = await _mediator.Send(request);

        return Ok(newCategory);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateCategoryModel model)
    {
        var request = new UpdateCategoryRequest
        {
            Id = id,
            EditedCategory = new Category
            {
                Name = model.Name
            }
        };

        var updatedCategory = await _mediator.Send(request);

        return Ok(updatedCategory);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var request = new DeleteCategoryRequest { Id = id };
        await _mediator.Send(request);

        return Ok();
    }
}