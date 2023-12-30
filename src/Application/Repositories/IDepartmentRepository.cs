using Application.DomainModels;

namespace Application.Repositories;

public interface IDepartmentRepository
{
    Task<IEnumerable<DepartmentListModel>> GetAllAsync();
    
    Task<UpdateDepartmentModel?> GetForEditAsync(int id);
    Task CreateAsync(CreateDepartmentModel model);
    
    Task UpdateAsync(UpdateDepartmentModel model);

    Task DeleteAsync(int id);
}
