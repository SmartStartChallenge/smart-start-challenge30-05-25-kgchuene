namespace EventsBackEnd.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using EventsBackEnd.Models;

    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the Event entity and its relationships
            // Example:
            // builder.Entity<Event>()
            //     .HasOne(e => e.Host)
            //     .WithMany()
            //     .HasForeignKey(e => e.HostId)
            //     .OnDelete(DeleteBehavior.Restrict);

            // builder.Entity<Event>()
            //     .HasMany(e => e.Attendees)
            //     .WithMany();
        }
    }
}
