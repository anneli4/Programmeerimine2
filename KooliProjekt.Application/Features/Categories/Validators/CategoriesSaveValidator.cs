using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Categories.Validators
{
    public class CategorySaveValidator : AbstractValidator<Category>
    {
        public CategorySaveValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        }
    }
}
