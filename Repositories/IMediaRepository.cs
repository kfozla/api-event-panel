using api_event_panel.Models;

namespace api_event_panel.Repositories;

public interface IMediaRepository
{
    Task SaveMedia(MediaModel mediaModel);
    Task<MediaModel> GetMedia(int id);
    Task<List<MediaModel>> GetMedias();
    Task <List <MediaModel>> GetMediasByEventId(int id);
    Task DeleteMedia(int id);
    Task <List<MediaModel>> GetMediaByUserId(int userId);
}