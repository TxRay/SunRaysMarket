namespace SunRaysMarket.Server.Core.DomainModels;

public record UpdateCustomerPreferencesModel
{
    public int PreferredStoreId { get; init; }
}