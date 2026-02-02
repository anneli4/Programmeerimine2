using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Orders
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly OrderRepository _repository;

        public CreateOrderHandler(OrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Date = request.Date,
                ClientId = request.ClientId,
                Discount = request.Discount
            };

            var createdOrder = await _repository.AddAsync(order);
            return createdOrder.Id;
        }
    }
}

