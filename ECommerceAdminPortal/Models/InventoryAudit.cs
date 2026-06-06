namespace ECommerceAdminPortal.Models;

public class InventoryAudit
{
    public int AuditId { get; set; }

    public int ProductId { get; set; }

    public int OldQuantity { get; set; }

    public int NewQuantity { get; set; }

    public DateTime ModifiedDate { get; set; }
}