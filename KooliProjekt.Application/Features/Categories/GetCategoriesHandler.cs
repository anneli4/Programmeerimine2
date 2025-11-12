using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Categories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategoriesQuery, PagedResult<Category>>
    {
        private readonly ApplicationDbContext _context;

        public GetCategoriesHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Categories.AsQueryable();
            return await query.GetPagedAsync(request.Page, request.PageSize);
        }
    }
}
