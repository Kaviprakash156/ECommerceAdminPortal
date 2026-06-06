using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Vendors;

public class DetailsModel : PageModel
{
    private readonly VendorService _vendorService;

    public DetailsModel(VendorService vendorService)
    {
        _vendorService = vendorService;
    }

    public Vendor Vendor { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Vendor = await _vendorService.GetByIdAsync(id);

        if (Vendor == null)
            return NotFound();

        return Page();
    }
}