using Application.DomainModels.BaseModels;

namespace Application.DomainModels;

public class UpdateDepartmentModel : CreateDepartmentModel
{
    public int Id { get; set; }
}
