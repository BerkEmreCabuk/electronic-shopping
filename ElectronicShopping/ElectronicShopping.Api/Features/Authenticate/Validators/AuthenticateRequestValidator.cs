using ElectronicShopping.Api.Features.Authenticate.Models;
using FluentValidation;

namespace ElectronicShopping.Api.Features.Authenticate.Validators
{
    public class AuthenticateRequestValidator : AbstractValidator<AuthenticateRequestModel>
    {
        public AuthenticateRequestValidator()
        {
            RuleFor(x => x).Empty().WithMessage("Model is not null or empty");
            RuleFor(x => x.username).Empty().WithMessage("Username is not null or empty");
            RuleFor(x => x.password).Empty().WithMessage("Password is not null or empty");
        }
    }
}
