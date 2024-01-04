using Application.DomainModels;

namespace Application.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<ProductListModel>> GetAllAsync();
    Task<IEnumerable<ProductListModel>> GetAllAsync(string listTitle, int? storeId);
    Task<IEnumerable<ProductListModel>> GetAllAsync(string listType, int departmentId);
    Task<IEnumerable<ProductListModel>> GetAllAsync(int departmentId);
    Task<IEnumerable<ProductListModel>> GetAllSearchAsync(string? queryString);
    Task<ProductDetailsModel?> GetAsync(int id);
    Task<CreateProductModel?> GetForEditAsync(int id);
    Task CreateAsync(CreateProductModel model);
    Task UpdateAsync(UpdateProductModel model);
    Task DeleteAsync(int id);
}
