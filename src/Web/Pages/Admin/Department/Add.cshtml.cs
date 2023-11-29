using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Department;

public class Add(IUnitOfWork unitOfWork) : PageModel
{
    [BindProperty]
    public CreateDepartmentModel NewDepartment { get; set; } = new();
    
    public void OnGet()
    {
        
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        await unitOfWork.DepartmentRepository.CreateAsync(NewDepartment);
        await unitOfWork.SaveChangesAsync();
        
        return RedirectToPage("/Admin/Department/Index");
    }
}