using api_event_panel.Models;
using api_event_panel.Repositories;

namespace api_event_panel.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMediaRepository _mediaRepository;

    public UserService(IUserRepository repository, IMediaRepository mediaRepository)
    {
        _repository = repository;
        _mediaRepository = mediaRepository;
    }

    public async Task<List<UserModel>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<UserModel> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    
    public async Task AddUser(UserModel user)
    {
        await _repository.AddUser(user);
    }

    public async Task DeleteUser(int id)
    {
        await _repository.DeleteUser(id);
    }

    public async Task<List <UserModel>> GetUsersByEvent(int eventId)
    {
       return await _repository.GetUsersByEvent(eventId);
    }

    public async Task<int> GetUserMediaCount(int userId)
    {
        var mediaList=await _mediaRepository.GetMediaByUserId(userId);
        return mediaList.Count;
    }

    public async Task<List<MediaModel>> GetUserMediaList(int userId)
    {
        return await _mediaRepository.GetMediaByUserId(userId);
    }
}