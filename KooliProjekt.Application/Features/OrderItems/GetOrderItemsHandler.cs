using MediatR;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class GetOrderItemsHandler : IRequestHandler<GetOrderItemsQuery, List<OrderItemDto>>
    {
        private readonly OrderItemRepository _repository;

        public GetOrderItemsHandler(OrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderItemDto>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _repository.GetAllAsync();

            return items.Select(i => new OrderItemDto
            {
                Id = i.Id,
                OrderId = i.OrderId,
                ItemId = i.ItemId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                Discount = i.Discount,
                Total = i.Total
            }).ToList();
        }
    }
}

