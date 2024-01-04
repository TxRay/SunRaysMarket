using Application.DomainModels;
using Application.UnitOfWork;

namespace Application.Services;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ProductDetailsModel?> GetProductDetailsAsync(int productId)
        => await unitOfWork.ProductRepository.GetAsync(productId);

    public async Task<IEnumerable<ProductListModel>> GetAllProductsAsync()
        => await unitOfWork.ProductRepository.GetAllAsync();

    public async Task<IEnumerable<ProductListModel>> GetAllProductsAsync(int departmentId)
        => await unitOfWork.ProductRepository.GetAllAsync(departmentId);

    public async Task<IEnumerable<ProductListModel>> SearchForProductsAsync(string? queryString)
        => await unitOfWork.ProductRepository.GetAllSearchAsync(queryString);
}