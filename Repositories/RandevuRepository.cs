using api_event_panel.Data;
using api_event_panel.Models;
using Microsoft.EntityFrameworkCore;

namespace api_event_panel.Repositories;

public class RandevuRepository: IRandevuRepository
{
    private readonly AppDbContext _db;

    public RandevuRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task Add(Randevu randevu)
    {
        _db.Randevu.Add(randevu);
        await _db.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        
        _db.Randevu.Remove(await _db.Randevu.FindAsync(id));
        await _db.SaveChangesAsync();
    }

    public async Task Update(Randevu randevu)
    {
         _db.Randevu.Update(randevu);
         await _db.SaveChangesAsync();
    }

    public async Task<List<Randevu>> GetAll()
    {
        return await _db.Randevu.ToListAsync();
    }

    public async Task<Randevu> GetById(int id)
    {
       return await _db.Randevu.FindAsync(id);
    }

    public async Task<List<Randevu>> GetByPanelUserId(int panelUserId)
    {
        return await _db.Randevu.Where(r=>r.panelUserId==panelUserId).ToListAsync();
    }
    
    
}