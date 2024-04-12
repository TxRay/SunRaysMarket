using SunRaysMarket.Shared.Core.DomainModels;

namespace SunRaysMarket.Shared.Core.Services;

public interface IProductService
{
    Task<ProductDetailsModel?> GetProductDetailsAsync(int productId);
    IAsyncEnumerable<ProductListModel?> GetAllProductsAsync();

    IAsyncEnumerable<ProductListModel?> GetAllProductsAsync(int departmentId);
}