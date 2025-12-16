using MediatR;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.Items
{
    public record GetItemsQuery() : IRequest<List<ItemDto>>;
}

