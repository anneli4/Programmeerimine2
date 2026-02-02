using MediatR;
using System;

namespace KooliProjekt.Application.Features.Orders
{
    public record CreateOrderCommand(
        DateTime Date,
        int ClientId,
        decimal Discount
    ) : IRequest<int>;
}

