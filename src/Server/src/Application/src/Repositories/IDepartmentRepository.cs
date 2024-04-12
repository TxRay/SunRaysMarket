using SunRaysMarket.Server.Core.DomainModels;

namespace SunRaysMarket.Server.Application.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<DepartmentListModel>> GetAllAsync();

    Task<UpdateDepartmentModel?> GetForEditAsync(int id);
    Task CreateAsync(CreateDepartmentModel model);

    Task UpdateAsync(UpdateDepartmentModel model);

    Task DeleteAsync(int id);
}