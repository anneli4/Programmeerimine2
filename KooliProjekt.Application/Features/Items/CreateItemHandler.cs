using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class CreateItemHandler : IRequestHandler<CreateItemCommand, int>
    {
        private readonly ItemRepository _repository;

        public CreateItemHandler(ItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = new Item
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                Photo = request.Photo
            };

            var createdItem = await _repository.AddAsync(item);
            return createdItem.Id;
        }
    }
}
