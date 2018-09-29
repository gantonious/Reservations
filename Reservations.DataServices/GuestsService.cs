using System;
using Reservations.Database;
using Reservations.Database.Models;
using Reservations.DataServices.Models;

namespace Reservations.DataServices
{
    public class GuestsService
    {
        private readonly ReservationsContext _reservationsContext;

        public GuestsService(ReservationsContext reservationsContext)
        {
            _reservationsContext = reservationsContext;
        }

        private GuestTableEntry BuildGuest(string name, int maxExtras)
        {
            return new GuestTableEntry
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Status = GuestStatus.NO_RESPONSE,
                TotalExtras = maxExtras,
            };
        }
    }
}