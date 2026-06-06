using ECommerceAdminPortal.Models;

namespace ECommerceAdminPortal.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Vendors.Any())
            return;

        context.Vendors.AddRange(
            new Vendor
            {
                VendorName = "Dell",
                Email = "sales@dell.com",
                Phone = "1111111111",
                IsActive = true,
                CreatedDate = DateTime.Now
            },
            new Vendor
            {
                VendorName = "HP",
                Email = "sales@hp.com",
                Phone = "2222222222",
                IsActive = true,
                CreatedDate = DateTime.Now
            });

        context.SaveChanges();
    }
}