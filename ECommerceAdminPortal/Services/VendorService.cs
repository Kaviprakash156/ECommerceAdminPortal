using ECommerceAdminPortal.Data;
using ECommerceAdminPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAdminPortal.Services;

public class VendorService
{
    private readonly ApplicationDbContext _context;

    public VendorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Vendor>> GetAllAsync()
    {
        return await _context.Vendors
            .OrderBy(x => x.VendorName)
            .ToListAsync();
    }

    public async Task<Vendor?> GetByIdAsync(int id)
    {
        return await _context.Vendors
            .FirstOrDefaultAsync(x => x.VendorId == id);
    }

    public async Task AddAsync(Vendor vendor)
    {
        _context.Vendors.Add(vendor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Vendor vendor)
    {
        _context.Vendors.Update(vendor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Vendor vendor)
    {
        _context.Vendors.Remove(vendor);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Vendor>> SearchAsync(string? searchTerm)
    {
        var query = _context.Vendors.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x =>
                x.VendorName.Contains(searchTerm));
        }

        return await query
            .OrderBy(x => x.VendorName)
            .ToListAsync();
    }
}