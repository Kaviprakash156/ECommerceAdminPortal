using System.ComponentModel.DataAnnotations;

namespace ECommerceAdminPortal.ViewModels;

public class OrderViewModel
{
    public int OrderId { get; set; }

    [Required]
    public string CustomerName { get; set; }
        = string.Empty;

    public string Status { get; set; }
        = "Pending";
}