using ECommerceAdminPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAdminPortal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Vendor> Vendors => Set<Vendor>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<Inventory> Inventories => Set<Inventory>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderDetail> OrderDetails => Set<OrderDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureVendor(modelBuilder);
        ConfigureProduct(modelBuilder);
        ConfigureInventory(modelBuilder);
        ConfigureOrder(modelBuilder);
        ConfigureOrderDetail(modelBuilder);
    }

    private static void ConfigureVendor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(v => v.VendorId);

            entity.Property(v => v.VendorName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(v => v.Email)
                .HasMaxLength(100);

            entity.Property(v => v.Phone)
                .HasMaxLength(20);
        });
    }

    private static void ConfigureProduct(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.ProductId);

            entity.Property(p => p.ProductName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            entity.HasOne(p => p.Vendor)
                .WithMany(v => v.Products)
                .HasForeignKey(p => p.VendorId);
        });
    }

    private static void ConfigureInventory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(i => i.InventoryId);

            entity.HasOne(i => i.Product)
                .WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(i => i.ProductId);
        });
    }

    private static void ConfigureOrder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.OrderId);

            entity.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");
        });
    }

    private static void ConfigureOrderDetail(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(od => od.OrderDetailId);

            entity.Property(od => od.Price)
                .HasColumnType("decimal(18,2)");

            entity.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            entity.HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);
        });
    }
}