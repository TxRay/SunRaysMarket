namespace SunRaysMarket.Shared.Core.DomainModels;

public record UpdateCustomerPreferencesModel
{
    public int PreferredStoreId { get; init; }
}
