using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoicesHandler : IRequestHandler<GetInvoicesQuery, PagedResult<Invoice>>
    {
        private readonly ApplicationDbContext _context;

        public GetInvoicesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Invoice>> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Invoices.AsQueryable();
            return await query.GetPagedAsync(request.Page, request.PageSize);
        }
    }
}

