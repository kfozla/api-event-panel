using api_event_panel.Models;
using api_event_panel.Repositories;

namespace api_event_panel.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
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
}