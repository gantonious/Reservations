using System;
using Reservations.Database;
using Reservations.Database.Models;

namespace Reservations.DataServices
{
    public class GuestsService
    {
        private readonly ReservationsContext _reservationsContext;

        public GuestsService(ReservationsContext reservationsContext)
        {
            _reservationsContext = reservationsContext;
        }

        private Guest BuildGuest(string name, int maxExtras)
        {
            return new Guest
            {
                Id = Guid.NewGuid(),
                Name = name,
                TotalExtras = maxExtras,
            };
        }
    }
}