using FluentValidation;

namespace KooliProjekt.Application.Features.Invoices.Validators
{
    public class DeleteInvoiceValidator : AbstractValidator<int>
    {
        public DeleteInvoiceValidator()
        {
            RuleFor(id => id).GreaterThan(0).WithMessage("Invoice ID must be greater than 0.");
        }
    }
}

