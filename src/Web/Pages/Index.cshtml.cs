using Application.DomainModels;
using Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public List<ProductListModel> FeaturedProducts { get; set; } = new();
    
    public IndexModel(ILogger<IndexModel> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        FeaturedProducts = (await _unitOfWork.ProductRepository.GetAllAsync()).ToList();
        
        return Page();
    }
}
