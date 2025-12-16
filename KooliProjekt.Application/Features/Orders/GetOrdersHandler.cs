using MediatR;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Orders
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, List<OrderDto>>
    {
        private readonly OrderRepository _repository;

        public GetOrdersHandler(OrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetAllAsync();

            return orders.Select(o => new OrderDto
            {
                Id = o.Id,
                Date = o.Date,
                ClientId = o.ClientId,
                Discount = o.Discount
            }).ToList();
        }
    }
}
