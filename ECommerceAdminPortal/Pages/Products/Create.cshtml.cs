using ECommerceAdminPortal.Data;
using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using ECommerceAdminPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Products;

public class CreateModel : PageModel
{
    private readonly ProductService _productService;
    private readonly ApplicationDbContext _context;

    public CreateModel(
        ProductService productService,
        ApplicationDbContext context)
    {
        _productService = productService;
        _context = context;
    }

    [BindProperty]
    public ProductViewModel Product { get; set; }
        = new();

    public SelectList Vendors { get; set; } = default!;

    public void OnGet()
    {
        LoadVendors();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadVendors();
            return Page();
        }

        var product = new Product
        {
            ProductName = Product.ProductName,
            Description = Product.Description,
            Price = Product.Price,
            VendorId = Product.VendorId,
            IsActive = Product.IsActive,
            CreatedDate = DateTime.Now
        };

        await _productService.AddAsync(product);

        TempData["Success"] =
            "Product created successfully.";

        return RedirectToPage("Index");
    }

    private void LoadVendors()
    {
        Vendors = new SelectList(
            _context.Vendors.ToList(),
            "VendorId",
            "VendorName");
    }
}