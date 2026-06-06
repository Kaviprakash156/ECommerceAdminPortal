using ECommerceAdminPortal.Data;
using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using ECommerceAdminPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Products;

public class EditModel : PageModel
{
    private readonly ProductService _productService;
    private readonly ApplicationDbContext _context;

    public EditModel(
        ProductService productService,
        ApplicationDbContext context)
    {
        _productService = productService;
        _context = context;
    }

    [BindProperty]
    public ProductViewModel Product { get; set; } = new();

    public SelectList Vendors { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await _productService.FindAsync(id);

        if (product == null)
            return NotFound();

        Product = new ProductViewModel
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Description = product.Description,
            Price = product.Price,
            VendorId = product.VendorId,
            IsActive = product.IsActive
        };

        LoadVendors();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            LoadVendors();
            return Page();
        }

        var product =
            await _productService.FindAsync(Product.ProductId);

        if (product == null)
            return NotFound();

        product.ProductName = Product.ProductName;
        product.Description = Product.Description;
        product.Price = Product.Price;
        product.VendorId = Product.VendorId;
        product.IsActive = Product.IsActive;

        await _productService.UpdateAsync(product);

        TempData["Success"] =
            "Product updated successfully";

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