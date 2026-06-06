using System.ComponentModel.DataAnnotations;

namespace ECommerceAdminPortal.ViewModels;

public class VendorViewModel
{
    public int VendorId { get; set; }

    [Required]
    [StringLength(100)]
    public string VendorName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}