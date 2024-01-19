using Application.DomainModels;

namespace Application.Services;

public interface IProductService
{
    Task<ProductDetailsModel?> GetProductDetailsAsync(int productId);
    Task<IEnumerable<ProductListModel>> GetAllProductsAsync();

    Task<IEnumerable<ProductListModel>> GetAllProductsAsync(int departmentId);

    Task<IEnumerable<ProductListModel>> SearchForProductsAsync(string? queryString);
}
