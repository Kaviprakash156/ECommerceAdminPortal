using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Orders;

public class StatusModel : PageModel
{
    private readonly OrderService _service;

    public StatusModel(OrderService service)
    {
        _service = service;
    }

    [BindProperty]
    public int OrderId { get; set; }

    [BindProperty]
    public string Status { get; set; }
        = string.Empty;

    public IActionResult OnGet(
        int id,
        string status)
    {
        OrderId = id;
        Status = status;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await _service.UpdateStatusAsync(
            OrderId,
            Status);

        TempData["Success"] =
            "Status updated successfully";

        return RedirectToPage("Index");
    }
}