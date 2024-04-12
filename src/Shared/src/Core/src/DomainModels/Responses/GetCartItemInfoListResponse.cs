namespace SunRaysMarket.Shared.Core.DomainModels.Responses;

public class GetCartItemInfoListResponse
{
    public IEnumerable<CartItemControlModel> CartItemInfoList { get; set; } = null!;
}