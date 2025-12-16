using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public record CreateOrderItemCommand(
        int OrderId,
        int ItemId,
        int Quantity,
        decimal UnitPrice,
        decimal Discount,
        decimal Total
    ) : IRequest<int>;
}

