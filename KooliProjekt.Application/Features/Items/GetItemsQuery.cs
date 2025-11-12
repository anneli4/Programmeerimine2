using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Items
{
    public class GetItemsQuery : IRequest<PagedResult<Item>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
