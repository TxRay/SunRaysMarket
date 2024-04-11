namespace SunRaysMarket.Server.Core.DomainModels;

public class UpdateUserModel
{
    private int Id { get; init; }
    public string Email { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public IEnumerable<string> Roles { get; init; } = new List<string>();
}
