using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Categories
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly CategoryRepository _repository;

        public CreateCategoryHandler(CategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description
            };

            var createdCategory = await _repository.AddAsync(category);
            return createdCategory.Id;
        }
    }
}


