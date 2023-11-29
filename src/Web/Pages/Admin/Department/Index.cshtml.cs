using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Deparment;

public class Index(IUnitOfWork unitOfWork) : PageModel
{
    public List<DepartmentListModel> Departments { get; private set; } = new();
    
    public async Task<IActionResult> OnGetAsync()
    {
        Departments = (await unitOfWork.DepartmentRepository.GetAllAsync()).ToList();
        
        return Page();
    }
}