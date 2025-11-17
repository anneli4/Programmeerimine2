using FluentValidation;

namespace KooliProjekt.Application.Features.Orders.Validators
{
    public class DeleteOrderValidator : AbstractValidator<int>
    {
        public DeleteOrderValidator()
        {
            RuleFor(id => id).GreaterThan(0).WithMessage("Order ID must be greater than 0.");
        }
    }
}
