using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Products;

public class DeleteModel : PageModel
{
    private readonly ProductService _productService;

    public DeleteModel(ProductService productService)
    {
        _productService = productService;
    }

    [BindProperty]
    public Product Product { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Product = await _productService.FindAsync(id);

        if (Product == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var product =
            await _productService.FindAsync(Product.ProductId);

        if (product == null)
            return NotFound();

        await _productService.DeleteAsync(product);

        TempData["Success"] =
            "Product deleted successfully";

        return RedirectToPage("Index");
    }
}