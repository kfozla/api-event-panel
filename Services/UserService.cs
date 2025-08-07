using api_event_panel.Dtos;
using api_event_panel.Models;
using api_event_panel.Repositories;

namespace api_event_panel.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMediaRepository _mediaRepository;
    private readonly IEventRepository _eventRepository;

    public UserService(IUserRepository repository, IMediaRepository mediaRepository, IEventRepository eventRepository)
    {
        _repository = repository;
        _mediaRepository = mediaRepository;
        _eventRepository = eventRepository;
    }

    public async Task<List<UserModel>> GetAll()
    {
        return await _repository.GetAll();
    }

    public async Task<UserModel> GetById(int id)
    {
        return await _repository.GetById(id);
    }

    
    public async Task AddUser(UserModelRequest user)
    {
        UserModel userModel = new UserModel()
        {
            Username = user.Username,
            SessionId = user.sessionId,
            EventId = user.eventId,
            MediaList = new List<MediaModel>()
        };
        await _repository.AddUser(userModel);
    }

    public async Task DeleteUser(int jwtUserId,int id)
    {
        var user = await _repository.GetById(id);
        if (user == null)
            throw new Exception($"User with id {id} not found");
        var relatedEvent = await _eventRepository.GetEvent(user.EventId);
        if (relatedEvent.PanelUserId != jwtUserId)
            throw new UnauthorizedAccessException("Not allowed to delete this user");

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

    public async Task<UserModel> GetUserByUsernameAndSessionId(string username, string sessionId)
    {
        return await _repository.GetByUsernameAndSession(username, sessionId);
    }
}