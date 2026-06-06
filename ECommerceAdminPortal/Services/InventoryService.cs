using ECommerceAdminPortal.Data;
using ECommerceAdminPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAdminPortal.Services;

public class InventoryService
{
    private readonly ApplicationDbContext _context;

    public InventoryService(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Inventory>> GetAllAsync()
    {
        return await _context.Inventories
            .Include(x => x.Product)
            .OrderBy(x => x.Product!.ProductName)
            .ToListAsync();
    }

    public async Task<Inventory?> GetByIdAsync(int id)
    {
        return await _context.Inventories
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x =>
                x.InventoryId == id);
    }

    public async Task UpdateAsync(Inventory inventory)
    {
        inventory.LastUpdated = DateTime.Now;

        _context.Inventories.Update(inventory);

        await _context.SaveChangesAsync();
    }

    public async Task<List<Inventory>>
        GetLowStockAsync()
    {
        return await _context.Inventories
            .Include(x => x.Product)
            .Where(x =>
                x.Quantity <= x.ReorderLevel)
            .ToListAsync();
    }

    public async Task<InventorySummary?>
GetInventorySummaryAsync()
    {
        return await _context.InventorySummaries
            .FromSqlRaw("EXEC sp_GetInventorySummary")
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }
}