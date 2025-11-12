using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class GetInvoiceLinesHandler : IRequestHandler<GetInvoiceLinesQuery, PagedResult<Invoice_Line>>
    {
        private readonly ApplicationDbContext _context;

        public GetInvoiceLinesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Invoice_Line>> Handle(GetInvoiceLinesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Invoice_Lines.AsQueryable();
            return await query.GetPagedAsync(request.Page, request.PageSize);
        }
    }
}
