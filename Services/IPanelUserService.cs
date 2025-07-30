using api_event_panel.Models;

namespace api_event_panel.Services;

public interface IPanelUserService
{
    Task<List<PanelUserModel>> GetAllPanelUsers();
    Task<PanelUserModel> GetPanelUser(int id);
    Task AddPanelUser(PanelUserModel panelUser);
    Task DeletePanelUser(int id);
    Task<List<EventModel>> GetAllEvents(int id);
    Task UploadProfilePicture(int panelUserId,IFormFile file);
}