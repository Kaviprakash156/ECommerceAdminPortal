using ECommerceAdminPortal.Data;
using ECommerceAdminPortal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAdminPortal.Pages.Inventory;

public class AuditModel : PageModel
{
    private readonly ApplicationDbContext _context;

    public AuditModel(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public IList<InventoryAudit> Audits
        = new List<InventoryAudit>();

    public async Task OnGetAsync()
    {
        Audits = await _context.InventoryAudits
            .OrderByDescending(x => x.ModifiedDate)
            .ToListAsync();
    }
}