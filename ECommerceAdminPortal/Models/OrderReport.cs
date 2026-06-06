namespace ECommerceAdminPortal.Models;

public class OrderReport
{
    public int OrderId { get; set; }

    public string CustomerName { get; set; }
        = string.Empty;

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; }
        = string.Empty;
}