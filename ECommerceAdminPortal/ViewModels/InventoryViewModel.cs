using System.ComponentModel.DataAnnotations;

namespace ECommerceAdminPortal.ViewModels;

public class InventoryViewModel
{
    public int InventoryId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    [Range(0, 999999)]
    public int Quantity { get; set; }

    [Range(1, 999999)]
    public int ReorderLevel { get; set; }
}