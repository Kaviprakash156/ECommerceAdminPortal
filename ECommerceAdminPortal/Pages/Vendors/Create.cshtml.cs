using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using ECommerceAdminPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Vendors;

public class CreateModel : PageModel
{
    private readonly VendorService _vendorService;

    public CreateModel(VendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [BindProperty]
    public VendorViewModel Vendor { get; set; }
        = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var vendor = new Vendor
        {
            VendorName = Vendor.VendorName,
            Email = Vendor.Email,
            Phone = Vendor.Phone,
            IsActive = Vendor.IsActive,
            CreatedDate = DateTime.Now
        };

        await _vendorService.AddAsync(vendor);

        TempData["Success"] =
            "Vendor created successfully";

        return RedirectToPage("Index");
    }
}