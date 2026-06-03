using Microsoft.EntityFrameworkCore;

namespace ECommerceAdminPortal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}