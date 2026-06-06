using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using ECommerceAdminPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Vendors;

public class EditModel : PageModel
{
    private readonly VendorService _vendorService;

    public EditModel(VendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [BindProperty]
    public VendorViewModel Vendor { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var vendor =
            await _vendorService.GetByIdAsync(id);

        if (vendor == null)
            return NotFound();

        Vendor = new VendorViewModel
        {
            VendorId = vendor.VendorId,
            VendorName = vendor.VendorName,
            Email = vendor.Email,
            Phone = vendor.Phone,
            IsActive = vendor.IsActive
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var vendor =
            await _vendorService.GetByIdAsync(Vendor.VendorId);

        if (vendor == null)
            return NotFound();

        vendor.VendorName = Vendor.VendorName;
        vendor.Email = Vendor.Email;
        vendor.Phone = Vendor.Phone;
        vendor.IsActive = Vendor.IsActive;

        await _vendorService.UpdateAsync(vendor);

        TempData["Success"] =
            "Vendor updated successfully";

        return RedirectToPage("Index");
    }
}