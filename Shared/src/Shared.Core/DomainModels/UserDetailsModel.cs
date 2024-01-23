using SunRaysMarket.Shared.Core.Structs;

namespace SunRaysMarket.Shared.Core.DomainModels;

public class UserDetailsModel
{
    public int Id { get; init; }
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public AddressStruct Address { get; init; }

    public string FullName => $"{FirstName} {LastName}";
    public string FullNameReverse => $"{LastName}, {FirstName}";
}
