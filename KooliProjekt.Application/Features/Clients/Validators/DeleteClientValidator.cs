using FluentValidation;

namespace KooliProjekt.Application.Features.Clients.Validators
{
    public class DeleteClientValidator : AbstractValidator<int>
    {
        public DeleteClientValidator()
        {
            RuleFor(id => id).GreaterThan(0).WithMessage("Client ID must be greater than 0.");
        }
    }
}

