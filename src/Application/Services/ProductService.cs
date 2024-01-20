using Application.DomainModels;
using Application.UnitOfWork;

namespace Application.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ProductDetailsModel?> GetProductDetailsAsync(int productId) =>
        await unitOfWork.ProductRepository.GetAsync(productId);

    public IAsyncEnumerable<ProductListModel?> GetAllProductsAsync() =>
         unitOfWork.ProductRepository.GetAllAsync();

    public IAsyncEnumerable<ProductListModel?> GetAllProductsAsync(int departmentId) => 
        unitOfWork.ProductRepository.GetAllAsync(departmentId);

    public async Task<IEnumerable<ProductListModel>> SearchForProductsAsync(string? queryString) =>
        await unitOfWork.ProductRepository.GetAllSearchAsync(queryString);
}
