using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Invoices.Validators
{
    public class InvoiceSaveValidator : AbstractValidator<Invoice>
    {
        public InvoiceSaveValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Order ID must be valid.");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Invoice date is required.");
            RuleFor(x => x.TotalAmount).GreaterThanOrEqualTo(0).WithMessage("Total amount cannot be negative.");
        }
    }
}
