using System.Net.Mime;
using Microsoft.EntityFrameworkCore;
using api_event_panel.Models;
namespace api_event_panel.Data;


public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<EventModel> Events { get; set; }
    public DbSet<MediaModel> Media { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<PanelUserModel> PanelUsers { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>()
            .HasMany(u => u.MediaList)
            .WithOne(m => m.User)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<EventModel>()
            .HasMany(e => e.UserList)
            .WithOne(u => u.Event)
            .HasForeignKey(u => u.EventId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<PanelUserModel>()
            .HasMany(p => p.EventList)
            .WithOne(e => e.PanelUser)
            .HasForeignKey(e => e.PanelUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}
