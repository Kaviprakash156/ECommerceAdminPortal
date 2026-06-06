using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Orders;

public class DetailsModel : PageModel
{
    private readonly OrderService _service;

    public DetailsModel(OrderService service)
    {
        _service = service;
    }

    public Order Order { get; set; } = null!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Order = await _service.GetByIdAsync(id);

        if (Order == null)
            return NotFound();

        return Page();
    }
}