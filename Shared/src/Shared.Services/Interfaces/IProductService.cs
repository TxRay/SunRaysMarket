namespace SunRaysMarket.Shared.Services.Interfaces;

public interface IProductService
{
    Task<ProductDetailsModel?> GetProductDetailsAsync(int productId);
    IAsyncEnumerable<ProductListModel?> GetAllProductsAsync();

    IAsyncEnumerable<ProductListModel?> GetAllProductsAsync(int departmentId);
}
