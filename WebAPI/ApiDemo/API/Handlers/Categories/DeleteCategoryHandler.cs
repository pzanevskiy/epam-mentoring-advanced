using API.Requests.Categories;
using ApiDemo.Data;
using ApiDemo.Data.Models;
using MediatR;

namespace API.Handlers.Categories
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest>
    {
        private readonly DemoContext _demoContext;

        public DeleteCategoryHandler(DemoContext demoContext)
        {
            _demoContext = demoContext;
        }

        public async Task Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            var category =
                await _demoContext.FindAsync<Category>(request.Id, cancellationToken);

            if (category != null)
            {
                _demoContext.Remove(category);
                await _demoContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
