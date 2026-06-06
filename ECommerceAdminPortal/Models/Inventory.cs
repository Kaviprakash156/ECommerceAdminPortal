namespace ECommerceAdminPortal.Models;

public class Inventory
{
    public int InventoryId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public int ReorderLevel { get; set; }

    public DateTime LastUpdated { get; set; }

    public Product? Product { get; set; }
}