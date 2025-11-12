using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Orders
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, PagedResult<Order>>
    {
        private readonly ApplicationDbContext _context;

        public GetOrdersHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Order>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Orders.AsQueryable();
            return await query.GetPagedAsync(request.Page, request.PageSize);
        }
    }
}
