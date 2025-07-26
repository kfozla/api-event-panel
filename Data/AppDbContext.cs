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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Fluent API ile ilişki tanımı
        modelBuilder.Entity<MediaModel>()
            .HasOne(m => m.EventModel) // Media'nın bir Event'i var
            .WithMany(e => e.MediaList) // Event'in birden fazla Media'sı var
            .HasForeignKey("EventModelId") // foreign key property'si
            .OnDelete(DeleteBehavior.Cascade); // Event silinirse medyalar da silinsin
        
        modelBuilder.Entity<MediaModel>()
            .HasOne(m => m.User)
            .WithMany(u => u.MediaList)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
