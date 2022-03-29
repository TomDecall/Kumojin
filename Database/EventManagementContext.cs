using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class EventManagementContext : DbContext
    {
        public DbSet<Event>? Events { get; set; }

        public EventManagementContext()
        {
        }

        public EventManagementContext(DbContextOptions<EventManagementContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(events =>
            {
                events.HasKey(b => b.Id);
                events.Property(b => b.Name).HasMaxLength(32);
                events.Property(b => b.Description);
                events.Property(b => b.StartDate);
                events.Property(b => b.EndDate);
            });
        }
    }
}
