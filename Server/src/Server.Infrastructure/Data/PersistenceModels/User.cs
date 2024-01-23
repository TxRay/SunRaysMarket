namespace SunRaysMarket.Server.Infrastructure.Data.PersistenceModels;

internal class User : IdentityUser<int>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public Address? Address { get; set; }
    public Customer? Customer { get; set; }
}
