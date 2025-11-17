using FluentValidation;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Features.Items.Validators
{
    public class ItemSaveValidator : AbstractValidator<Item>
    {
        public ItemSaveValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Item Name is required.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Category ID must be valid.");
        }
    }
}
