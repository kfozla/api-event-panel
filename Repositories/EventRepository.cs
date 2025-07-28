using api_event_panel.Models;
using Microsoft.EntityFrameworkCore;
using api_event_panel.Data;

namespace api_event_panel.Repositories;

public class EventRepository: IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task SaveEvent(EventModel eventModel)
    {
        foreach (var user in eventModel.UserList)
        {
            user.EventId = eventModel.Id;

            foreach (var media in user.MediaList)
            {
                media.UserId = user.Id;
            }
        }
        // 1. EventModel'ü kaydet
        _context.Events.Add(eventModel);
        await _context.SaveChangesAsync();
        
    }


    public async Task<List<EventModel>> GetEvents()
    {
        return await _context.Events.ToListAsync();
        
    }

    public async Task<EventModel> GetEvent(int id)
    {
        return await _context.Events.FindAsync(id);
        
    }

    public async Task UpdateEvent(EventModel eventModel)
    {
        _context.Events.Update(eventModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteEvent(int id)
    {
        _context.Events.Remove(await _context.Events.FindAsync(id));
        await _context.SaveChangesAsync();
    }

   
}