using api_event_panel.Data;
using api_event_panel.Models;
using Microsoft.EntityFrameworkCore;

namespace api_event_panel.Repositories;

public class ServicePackageRepository: IServicePackageRepository
{
    private readonly AppDbContext _context;

    public ServicePackageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ServicePackageModel> GetServicePackage(int servicePackageId)
    {
        return await _context.ServicePackages.FindAsync(servicePackageId);
    }
    public async Task<List<ServicePackageModel>> GetServicePackages()
    {
        return await _context.ServicePackages.ToListAsync();
    }

    public async Task AddServicePackage(ServicePackageModel servicePackage)
    {
        await _context.ServicePackages.AddAsync(servicePackage);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateServicePackage(ServicePackageModel servicePackage)
    {
         _context.ServicePackages.Update(servicePackage);
         await _context.SaveChangesAsync();
    }

    public async Task DeleteServicePackage(int servicePackageId)
    {
         _context.ServicePackages.Remove(_context.ServicePackages.Find(servicePackageId));
         await _context.SaveChangesAsync();
    }
}