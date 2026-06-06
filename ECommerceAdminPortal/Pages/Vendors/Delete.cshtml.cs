using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Vendors;

public class DeleteModel : PageModel
{
    private readonly VendorService _vendorService;

    public DeleteModel(VendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [BindProperty]
    public Vendor Vendor { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Vendor = await _vendorService.GetByIdAsync(id);

        if (Vendor == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var vendor =
            await _vendorService.GetByIdAsync(Vendor.VendorId);

        if (vendor == null)
            return NotFound();

        await _vendorService.DeleteAsync(vendor);

        TempData["Success"] =
            "Vendor deleted successfully";

        return RedirectToPage("Index");
    }
}