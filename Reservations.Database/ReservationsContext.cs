using Microsoft.EntityFrameworkCore;
using Reservations.Database.Models;

namespace Reservations.Database
{
    public class ReservationsContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Extra> Extras { get; set; }

        public ReservationsContext(DbContextOptions contextOptions) : base(contextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>()
                .HasMany<Extra>();
        }
    }
}