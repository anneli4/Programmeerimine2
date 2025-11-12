using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Clients
{
    public class GetClientsQuery : IRequest<PagedResult<Client>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}

