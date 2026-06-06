namespace ECommerceAdminPortal.Models;

public class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int VendorId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public Vendor? Vendor { get; set; }

    public Inventory? Inventory { get; set; }

    public ICollection<OrderDetail> OrderDetails { get; set; }
        = new List<OrderDetail>();
}