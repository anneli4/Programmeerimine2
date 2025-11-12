using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class GetOrderItemsQuery : IRequest<PagedResult<Order_Item>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

