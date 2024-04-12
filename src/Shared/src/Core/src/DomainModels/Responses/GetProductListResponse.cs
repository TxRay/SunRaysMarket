namespace SunRaysMarket.Shared.Core.DomainModels.Responses;

public class GetProductListResponse
{
    public IEnumerable<ProductListModel> Products { get; set; } = null!;
}