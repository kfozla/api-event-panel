using api_event_panel.Models;
using api_event_panel.Repositories;

namespace api_event_panel.Services;

public interface IUserService
{
    Task<List<UserModel>> GetAll();
    Task<UserModel> GetById(int id);
    Task AddUser(UserModel user);
    Task DeleteUser(int id);
    Task <List<UserModel>> GetUsersByEvent(int eventId);
}