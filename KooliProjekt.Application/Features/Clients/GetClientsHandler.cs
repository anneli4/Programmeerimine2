using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Clients
{
    public class GetClientsHandler : IRequestHandler<GetClientsQuery, PagedResult<Client>>
    {
        private readonly ApplicationDbContext _context;

        public GetClientsHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Client>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Clients.AsQueryable();
            return await query.GetPagedAsync(request.Page, request.PageSize);
        }
    }
}

