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
        // 1. EventModel'ü kaydet
        _context.Events.Add(eventModel);
        await _context.SaveChangesAsync();

        // eventModel.Id şimdi DB'den gelen PK oldu

        // 2. MediaModel'ların FK'sini set et
        if (eventModel.MediaList != null)
        {
            foreach (var media in eventModel.MediaList)
            {
                media.EventModelId = eventModel.Id;
                _context.Media.Add(media);
            }
        }

        if (eventModel.MediaList == null||eventModel.MediaList.Count == 0)
        {
            eventModel.MediaList = new List<MediaModel>
            {
                new MediaModel("english.png", "uploads/english.png", DateTime.Now, null, eventModel.Id,0),
                new MediaModel("german.png", "uploads/german.png", DateTime.Now, null, eventModel.Id,0),
                new MediaModel("russian.png", "uploads/russian.png", DateTime.Now, null, eventModel.Id,0)
            };
            foreach (var media in eventModel.MediaList)
            {
                _context.Media.Add(media);
            }
        }

        // 3. MediaModel'ları kaydet
        await _context.SaveChangesAsync();
    }


    public async Task<List<EventModel>> GetEvents()
    {
        return await _context.Events.ToListAsync();
        
    }

    public async Task<EventModel> GetEvent(int id)
    {
        return await _context.Events
            .Include(e => e.MediaList)
            .FirstOrDefaultAsync(e => e.Id == id);
        
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