using Microsoft.EntityFrameworkCore;
using Reservations.Database.Models;

namespace Reservations.Database
{
    public class ReservationsContext : DbContext
    {
        public DbSet<GuestTableEntry> Guests { get; set; }
        public DbSet<ExtraTableEntry> Extras { get; set; }

        public ReservationsContext(DbContextOptions contextOptions) : base(contextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestTableEntry>()
                .HasMany<ExtraTableEntry>();
        }
    }
}