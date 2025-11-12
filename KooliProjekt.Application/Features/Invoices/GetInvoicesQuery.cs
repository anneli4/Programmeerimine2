using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Invoices
{
    public class GetInvoicesQuery : IRequest<PagedResult<Invoice>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
