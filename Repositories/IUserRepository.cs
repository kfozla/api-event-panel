using api_event_panel.Models;

namespace api_event_panel.Repositories;

public interface IUserRepository
{
    Task<List<UserModel>> GetAll();
    Task<UserModel> GetById(int id);
    Task AddUser(UserModel user);
    Task DeleteUser(int id);
    Task<List<UserModel>> GetUsersByEvent(int eventId);
}