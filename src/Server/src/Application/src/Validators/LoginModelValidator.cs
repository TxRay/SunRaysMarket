using FluentValidation;

namespace SunRaysMarket.Server.Application.Validators;

public class LoginModelValidator : AbstractValidator<LoginModel>
{
    public LoginModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required.")
            .EmailAddress()
            .WithMessage("Email address is invalid.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}