using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class GetOrderItemsHandler : IRequestHandler<GetOrderItemsQuery, PagedResult<Order_Item>>
    {
        private readonly ApplicationDbContext _context;

        public GetOrderItemsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Order_Item>> Handle(GetOrderItemsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Order_Items.AsQueryable();
            return await query.GetPagedAsync(request.Page, request.PageSize);
        }
    }
}
