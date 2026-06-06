namespace ECommerceAdminPortal.Models;

public class InventorySummary
{
    public int TotalProducts { get; set; }

    public int OutOfStock { get; set; }

    public int LowStock { get; set; }
}