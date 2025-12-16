using MediatR;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.Orders
{
    public record GetOrdersQuery() : IRequest<List<OrderDto>>;
}

