using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.ProductType;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public List<ProductTypeDetailsModel> ProductTypes { get; set; } = new();

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnGetAsync()
    {
        ProductTypes = (await _unitOfWork.ProductTypeRepository.GetAllAsync()).ToList();
    }
}
