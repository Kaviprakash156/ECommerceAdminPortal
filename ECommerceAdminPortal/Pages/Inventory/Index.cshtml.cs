using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Inventory;

public class IndexModel : PageModel
{
    private readonly InventoryService _service;

    public IndexModel(InventoryService service)
    {
        _service = service;
    }

    public IList<Models.Inventory> Inventories
    { get; set; }
        = new List<Models.Inventory>();

    [BindProperty(SupportsGet = true)]
    public string? Status { get; set; }

    public async Task OnGetAsync()
    {
        if (Status == "lowstock")
        {
            Inventories =
                await _service.GetLowStockAsync();
        }
        else
        {
            Inventories =
                await _service.GetAllAsync();
        }
    }
}