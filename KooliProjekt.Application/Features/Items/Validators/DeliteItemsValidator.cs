using FluentValidation;

namespace KooliProjekt.Application.Features.Items.Validators
{
    public class DeleteItemValidator : AbstractValidator<int>
    {
        public DeleteItemValidator()
        {
            RuleFor(id => id).GreaterThan(0).WithMessage("Item ID must be greater than 0.");
        }
    }
}
