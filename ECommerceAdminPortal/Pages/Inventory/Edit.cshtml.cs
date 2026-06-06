using ECommerceAdminPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceAdminPortal.Pages.Inventory;

public class EditModel : PageModel
{
    private readonly InventoryService _service;

    public EditModel(InventoryService service)
    {
        _service = service;
    }

    [BindProperty]
    public Models.Inventory Inventory { get; set; }
        = null!;

    public async Task<IActionResult>
        OnGetAsync(int id)
    {
        Inventory =
            await _service.GetByIdAsync(id);

        if (Inventory == null)
            return NotFound();

        return Page();
    }

    public async Task<IActionResult>
        OnPostAsync()
    {
        await _service.UpdateAsync(Inventory);

        TempData["Success"] =
            "Stock adjusted successfully";

        return RedirectToPage("Index");
    }
}