using ECommerceAdminPortal.Data;
using ECommerceAdminPortal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ECommerceAdminPortal.Services;

public class OrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _context.Orders
            .OrderByDescending(x => x.OrderDate)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(x => x.OrderDetails)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x =>
                x.OrderId == id);
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);

        await _context.SaveChangesAsync();
    }
    public async Task CreateOrderAsync(
    string customerName,
    int productId,
    int quantity)
    {
        using IDbContextTransaction transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            var inventory =
                await _context.Inventories
                    .Include(x => x.Product)
                    .FirstOrDefaultAsync(x =>
                        x.ProductId == productId);

            if (inventory == null)
            {
                throw new Exception(
                    "Inventory not found");
            }

            if (inventory.Quantity < quantity)
            {
                throw new Exception(
                    "Insufficient stock");
            }

            decimal price =
                inventory.Product!.Price;

            decimal total =
                price * quantity;

            var order = new Order
            {
                CustomerName = customerName,
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = total
            };

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            var detail = new OrderDetail
            {
                OrderId = order.OrderId,
                ProductId = productId,
                Quantity = quantity,
                Price = price
            };

            _context.OrderDetails.Add(detail);

            inventory.Quantity -= quantity;

            inventory.LastUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public async Task UpdateStatusAsync(
    int orderId,
    string status)
    {
        var order =
            await _context.Orders
                .FirstOrDefaultAsync(x =>
                    x.OrderId == orderId);

        if (order == null)
            throw new Exception("Order not found");

        order.Status = status;

        await _context.SaveChangesAsync();
    }
    public async Task<List<OrderReport>>
GetOrderReportAsync()
    {
        return await _context.OrderReports
            .FromSqlRaw(
                "EXEC sp_GetOrderReport")
            .ToListAsync();
    }
}