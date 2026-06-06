using ECommerceAdminPortal.Data;
using ECommerceAdminPortal.Models;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ECommerceAdminPortal.Services;

public class ProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await _context.Products
            .Include(x => x.Vendor)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(x => x.Vendor)
            .FirstOrDefaultAsync(x => x.ProductId == id);
    }

    public async Task AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
    public async Task<Product?> FindAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(x => x.ProductId == id);
    }
    public async Task<List<Product>> SearchAsync(string? searchTerm)
    {
        var query = _context.Products
            .Include(x => x.Vendor)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x =>
                x.ProductName.Contains(searchTerm));
        }

        return await query.ToListAsync();
    }
    public async Task<List<Product>> GetPagedProductsAsync(
    string? searchTerm,
    int pageNumber,
    int pageSize)
    {
        var query = _context.Products
            .Include(x => x.Vendor)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x =>
                x.ProductName.Contains(searchTerm));
        }

        return await query
            .OrderBy(x => x.ProductName)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    public async Task<List<Product>> GetActiveProductsAsync()
    {
        return await _context.Products
            .Where(x => x.IsActive)
            .OrderBy(x => x.ProductName)
            .ToListAsync();
    }
}