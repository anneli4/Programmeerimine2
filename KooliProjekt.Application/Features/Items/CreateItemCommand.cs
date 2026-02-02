using MediatR;

namespace KooliProjekt.Application.Features.Items
{
    public record CreateItemCommand(
        int CategoryId,
        string Name,
        string Description,
        decimal Price,
        int Stock,
        string Photo
    ) : IRequest<int>;
}

