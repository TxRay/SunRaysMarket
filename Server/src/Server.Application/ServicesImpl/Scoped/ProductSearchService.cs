using SunRaysMarket.Server.Application.Services;
using SunRaysMarket.Server.Application.UnitOfWork;

namespace SunRaysMarket.Server.Application.ServicesImpl.Scoped;

public class ProductSearchService(IUnitOfWork unitOfWork) : IProductSearchService
{
    public IAsyncEnumerable<ProductListModel> GetSearchResults(string? searchString)
        => unitOfWork.ProductRepository.GetAllSearchAsync(searchString);
}