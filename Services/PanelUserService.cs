using api_event_panel.Models;
using api_event_panel.Repositories;

namespace api_event_panel.Services;

public class PanelUserService: IPanelUserService
{
    private readonly IPanelUserRepository _panelUserRepository;
    private readonly IEventRepository _eventRepository;

    public PanelUserService(IPanelUserRepository panelUserRepository)
    {
        _panelUserRepository = panelUserRepository;
    }
    public async Task<List<PanelUserModel>> GetAllPanelUsers()
    {
        return await _panelUserRepository.GetAllPanelUsers();
    }

    public async Task<PanelUserModel> GetPanelUser(int id)
    {
        return await _panelUserRepository.GetPanelUser(id);
    }

    public async Task AddPanelUser(PanelUserModel panelUser)
    {
        await _panelUserRepository.AddPanelUser(panelUser);
    }

    public async Task DeletePanelUser(int id)
    {
        await _panelUserRepository.DeletePanelUser(id);
    }

    public async Task<List<EventModel>> GetAllEvents(int panelUserId)
    {
        return await _eventRepository.GetPanelUserEvents(panelUserId);
    }

    public async Task UploadProfilePicture(int panelUserId,IFormFile file)
    {
        var folder ="uploads/profilepictures";
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);
        
        PanelUserModel panelUser = await _panelUserRepository.GetPanelUser(panelUserId);
        
        var extension = Path.GetExtension(file.FileName);
        var newFileName = panelUser.Username+ "-ProfilePicture" + extension;
        var filePath = Path.Combine(folder, newFileName);
        using (var filestream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(filestream);
        }
        await _panelUserRepository.UploadProfilePicture( panelUserId,filePath);
    }
    
}