using MediatR;
using KooliProjekt.Application.Dto;
using System.Collections.Generic;

namespace KooliProjekt.Application.Features.Categories
{
    public record GetCategoriesQuery() : IRequest<List<CategoryDto>>;
}
