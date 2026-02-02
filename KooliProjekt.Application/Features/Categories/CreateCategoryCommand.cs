using MediatR;

namespace KooliProjekt.Application.Features.Categories
{
    public record CreateCategoryCommand(
        string Name,
        string Description
    ) : IRequest<int>;
}
