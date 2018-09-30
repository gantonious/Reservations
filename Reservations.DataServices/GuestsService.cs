using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Guest> GetAllGuests()
        {
            return _reservationsContext.Guests
                .Select(g => BuildGuestFrom(g, GetExtrasForGuest(g.Id)));
        }

        public async Task<Guest> GetGuest(string id)
        {
            return await _reservationsContext.Guests
                .Where(g => g.Id == id)
                .Select(g => BuildGuestFrom(g, GetExtrasForGuest(g.Id)))
                .FirstOrDefaultAsync();
        }
        
        public async Task<Guest> SearchFor(string name)
        {
            return await _reservationsContext.Guests
                .Where(g => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .Select(g => BuildGuestFrom(g, GetExtrasForGuest(g.Id)))
                .FirstOrDefaultAsync();
        }

        public async Task AddGuestAsync(string name, int maxExtras)
        {
            var newGuest = BuildGuest(name, maxExtras);
            await _reservationsContext.Guests.AddAsync(newGuest);
            await _reservationsContext.SaveChangesAsync();
        }

        public async Task DeleteGuestAsync(string id)
        {
            var guest = _reservationsContext.Guests.First(g => g.Id == id);
            _reservationsContext.Guests.Remove(guest);
            await _reservationsContext.SaveChangesAsync();
        }

        private IEnumerable<ExtraTableEntry> GetExtrasForGuest(string guestId)
        {
            return _reservationsContext.Extras
                .Where(e => e.GuestTableEntryId == guestId);
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
                Status = status,
                Extras = extrasTableEntries.Select(e => e.Name)
            };
        }
    }
}