using Application.DomainModels;

namespace Application.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<DepartmentListModel>> GetAllAsync();
    Task CreateAsync(CreateDepartmentModel model);
}
