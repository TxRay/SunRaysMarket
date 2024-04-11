namespace SunRaysMarket.Server.Core.DomainModels;

public class CartDetailsModel
{
    public int Id { get; init; }
    public int? CustomerId { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }

    public bool HadCustomer => CustomerId is not null;
    public string FullName => $"{FirstName} {LastName}";
}
