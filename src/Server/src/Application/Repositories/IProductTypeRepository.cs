using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Application.Repositories;

public interface IProductTypeRepository
{
    public Task<IEnumerable<ProductTypeDetailsModel>> GetAllAsync();
    public Task<ProductTypeDetailsModel?> GetAsync(int id);
    public Task<CreateProductTypeModel?> GetForEditAsync(int id);
    public Task CreateAsync(CreateProductTypeModel model);
    public Task UpdateAsync(UpdateProductTypeModel model);
    public Task DeleteAsync(int id);
}
