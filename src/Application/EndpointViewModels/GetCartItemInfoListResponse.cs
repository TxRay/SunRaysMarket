using Application.DomainModels;

namespace Application.EndpointViewModels;

public class GetCartItemInfoListResponse
{
    public IEnumerable<CartItemControlModel> CartItemInfoList { get; set; } = null!;
}
