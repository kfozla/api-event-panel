using api_event_panel.Data;
using api_event_panel.Models;
using Microsoft.EntityFrameworkCore;

namespace api_event_panel.Repositories;

public class VillaRepository : IVillaRepository
{
    private readonly AppDbContext _context;
    public VillaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddVilla(VillaModel villa)
    {
        _context.Villa.Add(villa);
        await _context.SaveChangesAsync();
    }

    public async Task<List<VillaModel>> GetAllVillas()
    {
        return await _context.Villa.ToListAsync();
    }

    public async Task<VillaModel> GetVilla(int id)
    {
        var villaEntity = await _context.Villa.FindAsync(id);
        if (villaEntity == null)
            
            throw new KeyNotFoundException("Villa not found");
        return villaEntity;
    }
    public async Task<VillaModel> UpdateVilla(VillaModel villa)
    {
        var exists = await _context.Villa.AnyAsync(v => v.Id == villa.Id);

        if (!exists)
            throw new KeyNotFoundException("Villa not found");

        var villaEntity = _context.Villa.Update(villa);
        await _context.SaveChangesAsync();

        return villaEntity.Entity;
    }

    public async Task DeleteVilla(int id)
    {
        var villaToBeRemoved = await  _context.Villa.FindAsync(id);
        if (villaToBeRemoved != null)
        {
            _context.Villa.Remove(villaToBeRemoved);
            await _context.SaveChangesAsync();
        }
        
    }
}