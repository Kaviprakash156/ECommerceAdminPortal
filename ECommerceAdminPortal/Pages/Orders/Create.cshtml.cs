using ECommerceAdminPortal.Services;
using ECommerceAdminPortal.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Orders;

public class CreateModel : PageModel
{
    private readonly OrderService _orderService;
    private readonly ProductService _productService;

    public CreateModel(
        OrderService orderService,
        ProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    [BindProperty]
    public OrderCreateViewModel Order
    { get; set; } = new();

    public SelectList Products { get; set; }
        = default!;

    public async Task OnGetAsync()
    {
        await LoadProducts();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            await LoadProducts();
            return Page();
        }

        try
        {
            await _orderService.CreateOrderAsync(
                Order.CustomerName,
                Order.ProductId,
                Order.Quantity);

            TempData["Success"] =
                "Order created successfully";

            return RedirectToPage("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(
                string.Empty,
                ex.Message);

            await LoadProducts();

            return Page();
        }
    }

    private async Task LoadProducts()
    {
        Products = new SelectList(
            await _productService.GetActiveProductsAsync(),
            "ProductId",
            "ProductName");
    }
}