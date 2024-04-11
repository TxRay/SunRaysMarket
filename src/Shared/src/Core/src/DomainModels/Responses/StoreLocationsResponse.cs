namespace SunRaysMarket.Shared.Core.DomainModels.Responses;

public record StoreLocationsResponse
{
    public IEnumerable<StoreListModel> StoreLocations { get; init; } = [];
}
