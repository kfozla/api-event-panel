using api_event_panel.Dtos;
using api_event_panel.Models;

namespace api_event_panel.Services;

public interface IPanelUserService
{
    Task<List<PanelUserModel>> GetAllPanelUsers();
    Task<PanelUserModel> GetPanelUser(int id);
    Task AddPanelUser(PanelUserPostRequest panelUser);
    Task DeletePanelUser(int id);
    Task<List<EventModel>> GetAllEvents(int id);
    Task UploadProfilePicture(int panelUserId,IFormFile file);
    Task UpdateUser(int id, UpdateUser user);
    Task UpdateUser(PanelUserModel panelUser);
    Task ChangeServicePackage(int jwtUserId,int id, int servicePackageId);
}