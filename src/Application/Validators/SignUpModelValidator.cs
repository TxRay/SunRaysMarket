using Application.DomainModels;
using FluentValidation;

namespace Application.Validators;

public class SignUpModelValidator : AbstractValidator<SignUpModel>
{
    public SignUpModelValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email address is required.")
            .EmailAddress()
            .WithMessage("Email address is invalid.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .Length(8, 32)
            .WithMessage("Password must be between 8 and 32 characters.")
            .Matches("(?=.*[A-Z]).+")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches("(?=.*[a-z]).+")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches("(?=.*[0-9]).+")
            .WithMessage("Password must contain at least one number.")
            .Matches("(?=.*[^A-Za-z0-9\\s]).+")
            .WithMessage("Password must contain at least one special character.")
            .Matches("(?!.*\\s).+")
            .WithMessage("Password must not contain any whitespace.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .WithMessage("Confirm password is required.")
            .Equal(x => x.Password)
            .WithMessage("Confirm password must match password.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");


    }
}