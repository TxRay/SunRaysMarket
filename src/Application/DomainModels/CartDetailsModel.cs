using Application.Structs;

namespace Application.DomainModels;

public class CartDetailsModel
{
    public int Id { get; init; }
    public int CustomerId { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;

    public string FullName => $"{FirstName} {LastName}";
    public string FullNameReverse => $"{LastName}, {FirstName}";
}
