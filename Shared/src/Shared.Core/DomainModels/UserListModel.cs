namespace SunRaysMarket.Shared.Core.DomainModels;

public class UserListModel
{
    public int Id { get; init; }
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;

    public string FullName => $"{FirstName} {LastName}";
    public string FullNameReverse => $"{LastName}, {FirstName}";
}
