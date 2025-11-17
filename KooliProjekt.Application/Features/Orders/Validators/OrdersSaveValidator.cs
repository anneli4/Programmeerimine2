using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Orders.Validators
{
    public class OrderSaveValidator : AbstractValidator<Order>
    {
        public OrderSaveValidator()
        {
            RuleFor(x => x.ClientId).GreaterThan(0).WithMessage("Client ID must be valid.");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Order date is required.");
            RuleFor(x => x.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.");
        }
    }
}

