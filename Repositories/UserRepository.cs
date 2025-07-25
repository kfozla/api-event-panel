using api_event_panel.Data;
using api_event_panel.Models;
using Microsoft.EntityFrameworkCore;

namespace api_event_panel.Repositories;

public class UserRepository: IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<UserModel>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<UserModel> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    public async Task AddUser(UserModel user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        _context.Users.Remove(new UserModel { Id = id });
        await _context.SaveChangesAsync();
    }
}