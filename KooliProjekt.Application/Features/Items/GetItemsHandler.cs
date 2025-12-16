using MediatR;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class GetItemsHandler : IRequestHandler<GetItemsQuery, List<ItemDto>>
    {
        private readonly ItemRepository _repository;

        public GetItemsHandler(ItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ItemDto>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetAllAsync();

            return items.Select(i => new ItemDto
            {
                Id = i.Id,
                CategoryId = i.CategoryId,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                Stock = i.Stock,
                Photo = i.Photo
            }).ToList();
        }
    }
}

