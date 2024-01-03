using Application.DomainModels;

namespace Application.EndpointViewModels;

public class GetProductListResponse
{
    public IEnumerable<ProductListModel> Products { get; set; } = null!;
}