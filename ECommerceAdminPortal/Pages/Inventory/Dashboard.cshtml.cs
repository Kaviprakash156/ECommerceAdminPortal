using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Inventory;

public class DashboardModel : PageModel
{
    private readonly InventoryService _service;

    public DashboardModel(
        InventoryService service)
    {
        _service = service;
    }

    public InventorySummary Summary { get; set; }
        = new();

    public async Task OnGetAsync()
    {
        HttpContext.Session.SetString(
    "InventoryNotification",
    "Inventory Dashboard Accessed");

        ViewData["Notification"] =
    HttpContext.Session.GetString(
        "InventoryNotification");

        Summary =
            await _service.GetInventorySummaryAsync()
            ?? new InventorySummary();
    }
}