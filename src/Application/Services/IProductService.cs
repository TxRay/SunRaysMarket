using Application.DomainModels;

namespace Application.Services;

public interface IProductService
{
    Task<ProductDetailsModel?> GetProductDetailsAsync(int productId);
    IAsyncEnumerable<ProductListModel?> GetAllProductsAsync();
    
    
    IAsyncEnumerable<ProductListModel?> GetAllProductsAsync(int departmentId);

    Task<IEnumerable<ProductListModel>> SearchForProductsAsync(string? queryString);
}
