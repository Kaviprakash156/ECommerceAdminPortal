namespace ECommerceAdminPortal.ViewModels;

public class InventorySummaryViewModel
{
    public int TotalProducts { get; set; }

    public int OutOfStock { get; set; }

    public int LowStock { get; set; }
}