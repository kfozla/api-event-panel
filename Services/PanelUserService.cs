using api_event_panel.Dtos;
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

    public async Task AddPanelUser(PanelUserPostRequest panelUser)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(panelUser.Password);
        PanelUserModel panelUserModel = new PanelUserModel()
        {
            Username = panelUser.Username,
            FirstName = panelUser.FirstName,
            LastName = panelUser.LastName,
            Email = panelUser.Email,
            Password = hashedPassword,
            PhoneNumber = panelUser.PhoneNumber,
            Role = panelUser.Role,
            CreatedOn = DateTime.Now,
            ModifiedOn = DateTime.Now,
            DeletedOn = null,
            EventList = new List<EventModel>(),
        };
        await _panelUserRepository.AddPanelUser(panelUserModel);
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

    public async Task UpdateUser(int id, UpdateUser user)
    {
        var existingUser = await _panelUserRepository.GetPanelUser(id);
        existingUser.Username = user.userName;
        existingUser.FirstName = user.firstName;
        existingUser.LastName = user.lastName;
        existingUser.Email = user.email;
        existingUser.PhoneNumber = user.phoneNumber;
        existingUser.ModifiedOn = DateTime.Now;
        
        await _panelUserRepository.UpdateUser(existingUser);
    }

    public async Task UpdateUser(PanelUserModel panelUser)
    {
        await _panelUserRepository.UpdateUser(panelUser);
    }
    
}