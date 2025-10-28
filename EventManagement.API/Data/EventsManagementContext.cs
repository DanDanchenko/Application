using EventManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.API.Data
{
    public class EventsManagementContext : DbContext
    {
        public EventsManagementContext(DbContextOptions<EventsManagementContext> opts) : base(opts) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participant>().HasKey(p => new {p.UserId, p.EventId});
            modelBuilder.Entity<User>().HasMany(u => u.OrganizedEvents).WithOne(e => e.Organizer).HasForeignKey(e => e.OrganizerId);
            modelBuilder.Entity<Participant>().HasOne(p => p.User).WithMany(u => u.Participations).HasForeignKey(p => p.UserId);
            modelBuilder.Entity<Participant>().HasOne(p => p.Event).WithMany(e => e.Participants).HasForeignKey(p => p.EventId);
        }
    }
}
