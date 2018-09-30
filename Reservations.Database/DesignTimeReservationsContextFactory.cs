using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Reservations.Database
{
    public class DesignTimeReservationsContextFactory : IDesignTimeDbContextFactory<ReservationsContext>
    {
        private const string CONNECTION_STRING_ENV_VARIABLE = "DATABASE_CONNECTION_STRING";
        
        public ReservationsContext CreateDbContext(string[] args)
        {
            var connectionString = Environment.GetEnvironmentVariable(CONNECTION_STRING_ENV_VARIABLE);
            var dbContextOptions = new DbContextOptionsBuilder()
                .UseNpgsql(connectionString).Options;
            
            return new ReservationsContext(dbContextOptions);
        }
    }
}