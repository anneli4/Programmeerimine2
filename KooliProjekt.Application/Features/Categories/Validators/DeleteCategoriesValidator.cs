using FluentValidation;

namespace KooliProjekt.Application.Features.Categories.Validators
{
    public class DeleteCategoryValidator : AbstractValidator<int>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(id => id).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
        }
    }
}

