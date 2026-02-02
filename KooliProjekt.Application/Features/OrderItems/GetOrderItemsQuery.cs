using MediatR;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.OrderItems
{
    public record GetOrderItemsQuery() : IRequest<List<OrderItemDto>>;
}

