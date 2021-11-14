using ElectronicShopping.Api.Features.Cart.Models;
using FluentValidation;

namespace ElectronicShopping.Api.Features.Cart.Validators
{
    public class AddProductValidator : AbstractValidator<AddProductRequestModel>
    {
        public AddProductValidator()
        {
            RuleFor(x => x).Empty().WithMessage("Model is not null or empty");
            RuleFor(x => x.ItemId).LessThanOrEqualTo(0).WithMessage("ItemId is invalid");
            RuleFor(x => x.Quantity).LessThanOrEqualTo(0).WithMessage("Quantity must be greater than zero");
        }
    }
}
