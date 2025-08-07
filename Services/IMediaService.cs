using api_event_panel.Models;

namespace api_event_panel.Services;

public interface IMediaService
{
    Task SaveMedia(string username,string sessionId,List<IFormFile> mediaModel);
    Task<MediaModel> GetMedia(int id);
    Task<List<MediaModel>> GetMedias();
    Task <List <MediaModel>> GetMediasByEventId(int id);
    Task DeleteMedia(string username,string sessionId,int id,string? panelUserRole);
    Task <List<MediaModel>> GetMediaByUserId(int userId);
}