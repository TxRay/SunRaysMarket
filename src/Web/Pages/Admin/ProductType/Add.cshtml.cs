using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.ProductType;

public class Add : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public CreateProductTypeModel NewProductType { get; set; } = new();

    public List<DepartmentListModel> Departments { get; set; } = new();

    public Add(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        Departments = (await _unitOfWork.DepartmentRepository.GetAllAsync()).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _unitOfWork.ProductTypeRepository.CreateAsync(NewProductType);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToPage("Admin/ProductType/Index");
    }
}
