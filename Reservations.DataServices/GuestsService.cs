using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public Guest SearchFor(string name)
        {
            var guest = _reservationsContext.Guests
                .FirstOrDefault(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            
            if (guest == null)
            {
                return null;
            }

            var extras = _reservationsContext.Extras
                .Where(e => e.GuestId == guest.Id);

            return BuildGuestFrom(guest, extras);
        }
        
        public async Task AddGuestAsync(string name, int maxExtras)
        {
            var newGuest = BuildGuest(name, maxExtras);
            await _reservationsContext.Guests.AddAsync(newGuest);
            await _reservationsContext.SaveChangesAsync();
        }
            
        private static GuestTableEntry BuildGuest(string name, int maxExtras)
        {
            return new GuestTableEntry
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Status = GuestStatus.NO_RESPONSE.ToString(),
                TotalExtras = maxExtras,
            };
        }

        private static Guest BuildGuestFrom(GuestTableEntry guestTableEntry, IEnumerable<ExtraTableEntry> extrasTableEntries)
        {
            Enum.TryParse(guestTableEntry.Status, out GuestStatus status);

            return new Guest
            {
                Id = guestTableEntry.Id,
                Name = guestTableEntry.Name,
                MaxExtras = guestTableEntry.TotalExtras,
                Extras = extrasTableEntries.Select(e => e.Name)
            };
        }
    }
}