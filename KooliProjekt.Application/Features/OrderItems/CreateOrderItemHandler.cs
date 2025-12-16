using MediatR;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class CreateOrderItemHandler : IRequestHandler<CreateOrderItemCommand, int>
    {
        private readonly OrderItemRepository _repository;

        public CreateOrderItemHandler(OrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
        {
            var orderItem = new Order_Item
            {
                OrderId = request.OrderId,
                ItemId = request.ItemId,
                Quantity = request.Quantity,
                UnitPrice = request.UnitPrice,
                Discount = request.Discount,
                Total = request.Total
            };

            var createdItem = await _repository.AddAsync(orderItem);
            return createdItem.Id;
        }
    }
}

