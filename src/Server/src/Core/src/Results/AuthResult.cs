using System.ComponentModel.DataAnnotations;
using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Core.Results;

public abstract record AuthResult
{
    public UserDetailsModel? User { get; init; }

    public ValidationResult? ValidationResult { get; init; }

    public static AuthSuccess Success(UserDetailsModel user)
    {
        return new AuthSuccess(user, $"The user was successfully logged in as {user.FirstName}.");
    }

    public static AuthFailure Failure(string message, ValidationResult? validationResult = default)
    {
        if (validationResult is not null) return new AuthValidationFailure(validationResult, message);

        return new AuthExecutionFailure(message);
    }

    public record AuthNone : AuthResult;

    public abstract record AuthSome(string? Message) : AuthResult;

    public record AuthSuccess(UserDetailsModel User, string? Message = default) : AuthSome(Message);

    public abstract record AuthFailure(string? Message) : AuthSome(Message);

    public record AuthExecutionFailure(string? Message = default) : AuthFailure(Message);

    public record AuthValidationFailure(ValidationResult ValidationResult, string? Message = default)
        : AuthFailure(Message);
}