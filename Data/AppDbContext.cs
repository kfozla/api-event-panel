using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using api_event_panel.Models;
namespace api_event_panel.Data;


public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<EventModel> Events { get; set; }
}