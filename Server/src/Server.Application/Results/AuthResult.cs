using FluentValidation.Results;

namespace SunRaysMarket.Server.Application.Results;

public class AuthResult
{
    public UserDetailsModel? User { get; init; }
    public string Message { get; init; } = null!;

    public ValidationResult? ValidationResult { get; init; }

    public bool WasSuccessful => User is not null;
    public bool IsValidationFailure => ValidationResult is not null;

    public static AuthResult Success(UserDetailsModel user) =>
        new()
        {
            User = user,
            Message = $"The user was successfully logged in as {user.FirstName}."
        };

    public static AuthResult Failure(string message) => new() { Message = message };
}
