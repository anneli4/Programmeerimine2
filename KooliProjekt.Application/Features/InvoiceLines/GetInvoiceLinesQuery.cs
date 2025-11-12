using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.InvoiceLines
{
    public class GetInvoiceLinesQuery : IRequest<PagedResult<Invoice_Line>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
