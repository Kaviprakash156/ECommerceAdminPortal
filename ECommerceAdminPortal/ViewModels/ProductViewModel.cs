using System.ComponentModel.DataAnnotations;

namespace ECommerceAdminPortal.ViewModels;

public class ProductViewModel
{
    public int ProductId { get; set; }

    [Required]
    [StringLength(200)]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(1, 999999)]
    public decimal Price { get; set; }

    [Required]
    public int VendorId { get; set; }

    public bool IsActive { get; set; }
}