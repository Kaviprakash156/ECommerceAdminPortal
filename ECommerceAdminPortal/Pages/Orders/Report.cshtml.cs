using ECommerceAdminPortal.Models;
using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Orders;

public class ReportModel : PageModel
{
    private readonly OrderService _service;

    public ReportModel(OrderService service)
    {
        _service = service;
    }

    public IList<OrderReport> Orders
        = new List<OrderReport>();

    public async Task OnGetAsync()
    {
        Orders =
            await _service.GetOrderReportAsync();
    }
}