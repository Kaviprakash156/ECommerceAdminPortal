using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Products;

public class DetailsModel : PageModel
{
    private readonly ProductService _productService;

    public DetailsModel(ProductService productService)
    {
        _productService = productService;
    }

    public Product Product { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Product = await _productService.GetByIdAsync(id);

        if (Product == null)
            return NotFound();

        return Page();
    }
}