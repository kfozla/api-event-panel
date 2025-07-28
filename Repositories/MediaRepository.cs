using api_event_panel.Data;
using api_event_panel.Models;
using Microsoft.EntityFrameworkCore;

namespace api_event_panel.Repositories;

public class MediaRepository: IMediaRepository
{
    private readonly AppDbContext _context;

    public MediaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task SaveMedia(MediaModel media)
    {
       _context.Media.Add(media);
       await _context.SaveChangesAsync();
    }

    public async Task<MediaModel> GetMedia(int id)
    {
        return await _context.Media.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<MediaModel>> GetMedias()
    {
        return await _context.Media.ToListAsync();
    }

    public async Task<List<MediaModel>> GetMediasByEventId(int eventId)
    {
        return await _context.Users
            .Where(u => u.EventId == eventId)
            .SelectMany(u=>u.MediaList)
            .ToListAsync();
    }

    public async Task DeleteMedia(int id)
    {
        var media = await _context.Media.FirstOrDefaultAsync(m => m.Id == id);
        if (media != null)
        {
            // Dosya silme
            if (File.Exists(media.FilePath))
                File.Delete(media.FilePath);

            if (!string.IsNullOrEmpty(media.PosterPath) && File.Exists(media.PosterPath))
                File.Delete(media.PosterPath);

            _context.Media.Remove(media); // Aynı instance ile sil
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<MediaModel>> GetMediaByUserId(int userId)
    {
        return await _context.Media
            .Where(m => m.UserId == userId )
            .ToListAsync();
    }
    

}