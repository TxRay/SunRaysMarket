using SunRaysMarket.Server.Application.UnitOfWork;
using SunRaysMarket.Shared.Services.Interfaces;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task<ProductDetailsModel?> GetProductDetailsAsync(int productId)
    {
        return await unitOfWork.ProductRepository.GetAsync(productId);
    }

    public IAsyncEnumerable<ProductListModel?> GetAllProductsAsync()
    {
        return unitOfWork.ProductRepository.GetAllAsync();
    }

    public IAsyncEnumerable<ProductListModel?> GetAllProductsAsync(int departmentId)
    {
        return unitOfWork.ProductRepository.GetAllAsync(departmentId);
    }
}
