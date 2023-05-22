using API.Models;
using API.Requests.Items;
using ApiDemo.Data.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id, int page = 1, int size = 1)
        {
            var request = new GetFilteredItemsRequest { CategoryId = id, Page = page, PageSize = size };
            var result = await _mediator.Send(request);

            return result.Any() ? Ok(result) : NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateItemModel model)
        {
            var request = new CreateItemRequest()
            {
                Item = new Item
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId,
                }
            };

            var newItem = await _mediator.Send(request);

            return Ok(newItem);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateItemModel model)
        {
            var request = new UpdateItemRequest
            {
                Id = id,
                EditedItem = new Item
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    CategoryId = model.CategoryId
                }
            };

            var updatedItem = await _mediator.Send(request);

            return Ok(updatedItem);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new DeleteItemRequest { Id = id };
            await _mediator.Send(request);

            return Ok();
        }
    }
}