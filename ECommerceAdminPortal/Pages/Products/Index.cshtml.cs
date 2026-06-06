using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Products;

public class IndexModel : PageModel
{
    private readonly ProductService _productService;

    public IndexModel(ProductService productService)
    {
        _productService = productService;
    }

    public IList<Product> Products { get; set; }
        = new List<Product>();

    public async Task OnGetAsync()
    {
        ViewData["Title"] = "Product List";

        Products = await _productService.GetAllAsync();
    }
}