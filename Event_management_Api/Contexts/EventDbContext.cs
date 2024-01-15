using Event_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Management_System.Contexts;

public class EventDbContext : DbContext
{
    public EventDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Participant> Participants { get; set; }
}
