using Application.DomainModels;

namespace Application.Repositories;

public interface IProductRepository
{
    IAsyncEnumerable<ProductListModel> GetAllAsync();
    IAsyncEnumerable<ProductListModel> GetAllAsync(string listTitle, int? storeId);
    IAsyncEnumerable<ProductListModel> GetAllAsync(string listType, int departmentId);
    IAsyncEnumerable<ProductListModel> GetAllAsync(int departmentId);
    Task<IEnumerable<ProductListModel>> GetAllSearchAsync(string? queryString);
    Task<ProductDetailsModel?> GetAsync(int id);
    Task<CreateProductModel?> GetForEditAsync(int id);
    Task CreateAsync(CreateProductModel model);
    Task UpdateAsync(UpdateProductModel model);
    Task DeleteAsync(int id);
}
