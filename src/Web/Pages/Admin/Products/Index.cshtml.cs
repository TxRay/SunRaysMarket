using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Admin.Products;

public class Index : PageModel
{
    private readonly IUnitOfWork _unitOfWork;

    public List<ProductListModel> Products { get; private set; } = new();

    public Index(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task OnGetAsync()
    {
        Products = (await _unitOfWork.ProductRepository.GetAllAsync()).ToList();
    }
}
