using api_event_panel.Data;
using api_event_panel.Models;
using Microsoft.EntityFrameworkCore;

namespace api_event_panel.Repositories;

public class PanelUserRepository: IPanelUserRepository
{
    private readonly AppDbContext _context;

    public PanelUserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<PanelUserModel> GetPanelUser(int id)
    {
        return await _context.PanelUsers
            .Include(p => p.EventList) 
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async  Task<List<PanelUserModel>> GetAllPanelUsers()
    {
        return await _context.PanelUsers.ToListAsync();
    }

    public async Task DeletePanelUser(int id)
    {
         _context.PanelUsers.Remove(new PanelUserModel { Id = id });
         await _context.SaveChangesAsync();
    }

    public async Task AddPanelUser(PanelUserModel user)
    {
        foreach ( var etkinlik in user.EventList)
        {
            etkinlik.PanelUserId = user.Id;
            foreach (var kullanici in etkinlik.UserList)
            {
                kullanici.EventId = etkinlik.Id;
                foreach (var media in kullanici.MediaList)
                {
                    media.UserId = kullanici.Id;
                }
            }
        }
        _context.PanelUsers.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UploadProfilePicture(int panelUserId,string filePath)
    {
        PanelUserModel panelUser = GetPanelUser(panelUserId).Result;
        
        panelUser.ProfilePictureUrl = filePath;
        _context.PanelUsers.Update(panelUser);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateUser(PanelUserModel panelUser)
    {
        _context.PanelUsers.Update(panelUser);
        await _context.SaveChangesAsync();
    }
    
}