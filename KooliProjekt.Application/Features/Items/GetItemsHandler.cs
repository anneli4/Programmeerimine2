using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Items
{
    public class GetItemsHandler : IRequestHandler<GetItemsQuery, PagedResult<Item>>
    {
        private readonly ApplicationDbContext _context;

        public GetItemsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Items.AsQueryable();
            return await query.GetPagedAsync(request.Page, request.PageSize);
        }
    }
}
