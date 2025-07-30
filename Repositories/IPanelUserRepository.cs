using api_event_panel.Models;

namespace api_event_panel.Repositories;

public interface IPanelUserRepository
{
    Task<PanelUserModel> GetPanelUser( int id);
    Task<List<PanelUserModel>> GetAllPanelUsers();
    Task DeletePanelUser(int id);
    Task AddPanelUser(PanelUserModel panelUser);
    Task UploadProfilePicture(int panelUserId,string  filePath);
    
    
}