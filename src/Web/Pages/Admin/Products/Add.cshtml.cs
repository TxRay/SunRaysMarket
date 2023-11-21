using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.Pages.Admin.Products;

public class Add : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    [BindProperty]
    public CreateProductModel NewProduct { get; set; } = new();

    public IFormFile? ImageFile { get; set; }

    public List<SelectListItem> Departments { get; private set; } = new();

    public List<SelectListItem> ProductTypes { get; private set; } = new();

    public List<SelectListItem> UnitsOfMeasure { get; private set; } = new();

    public Add(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var departmentModels = await _unitOfWork.DepartmentRepository.GetAllAsync();
        var productTypeModels = await _unitOfWork.ProductTypeRepository.GetAllAsync();
        var unitOfMeasureModels = await _unitOfWork.UnitOfMeasureRepository.GetAllAsync();

        Departments = departmentModels
            .Select(d => new SelectListItem(d.Name, d.Id.ToString()))
            .ToList();
        ProductTypes = productTypeModels
            .Select(pt => new SelectListItem(pt.Name, pt.Id.ToString()))
            .ToList();
        UnitsOfMeasure = unitOfMeasureModels
            .Select(uom => new SelectListItem(uom.ToString(), uom.Id.ToString()))
            .ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ImageFile is null)
        {
            ModelState.AddModelError("ImageFile", "Image is required");
            return Page();
        }

        var urlIdentifier =await _unitOfWork.ImageRepository.UploadAsync(ImageFile);
        await _unitOfWork.SaveChangesAsync();
        
        var photoUrl =await _unitOfWork.ImageRepository.GetUrlAsync(Guid.Parse(urlIdentifier));
        
        if (photoUrl is null)
        {
            ModelState.AddModelError("ImageFile", "Image upload failed");
            return Page();
        }

        NewProduct.PhotoUrl = photoUrl;
        await _unitOfWork.ProductRepository.CreateAsync(NewProduct);
        await _unitOfWork.SaveChangesAsync();

        return RedirectToPage("/Admin/Products/Index");
    }
}
