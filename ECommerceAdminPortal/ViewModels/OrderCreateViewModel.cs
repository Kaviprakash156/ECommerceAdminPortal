using System.ComponentModel.DataAnnotations;

namespace ECommerceAdminPortal.ViewModels;

public class OrderCreateViewModel
{
    [Required]
    public string CustomerName { get; set; }
        = string.Empty;

    [Required]
    public int ProductId { get; set; }

    [Range(1, 100)]
    public int Quantity { get; set; }
}