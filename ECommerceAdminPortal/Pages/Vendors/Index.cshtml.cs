using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Vendors;

public class IndexModel : PageModel
{
    private readonly VendorService _vendorService;

    public IndexModel(VendorService vendorService)
    {
        _vendorService = vendorService;
    }

    public IList<Vendor> Vendors { get; set; }
        = new List<Vendor>();

    [BindProperty(SupportsGet = true)]
    public string? SearchTerm { get; set; }

    public async Task OnGetAsync()
    {
        ViewData["Title"] = "Vendor List";

        Vendors = await _vendorService
            .SearchAsync(SearchTerm);
    }
}