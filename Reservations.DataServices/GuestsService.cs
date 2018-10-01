using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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

        public async Task<Stats> GetStatsAsync()
        {
            var guestsThatHaveNotResponded = _reservationsContext.Guests
                .Where(g => g.Status == GuestStatus.NO_RESPONSE.ToString());
            
            var guestsNotAttending = _reservationsContext.Guests
                .Where(g => g.Status == GuestStatus.NOT_COMING.ToString());
            
            var guestsAttendingIds = _reservationsContext.Guests
                .Where(g => g.Status == GuestStatus.ATTENDING.ToString())
                .Select(g => g.Id);
            
            var extrasAttending = _reservationsContext.Extras
                .Where(e => guestsAttendingIds.Contains(e.GuestTableEntryId));

            return new Stats
            {
                TotalAttending = await guestsAttendingIds.CountAsync(),
                TotalExtras = await extrasAttending.CountAsync(),
                TotalUnresponsive = await guestsThatHaveNotResponded.CountAsync(),
                TotalNotAttending = await guestsNotAttending.CountAsync()
            };
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

        public async Task AddGuestAsync(AddGuestParameters addGuestParameters)
        {
            var newGuest = BuildGuest(addGuestParameters.Name, addGuestParameters.MaxExtras);
            await _reservationsContext.Guests.AddAsync(newGuest);
            await _reservationsContext.SaveChangesAsync();
        }
        
        public async Task AddGuestsAsync(IEnumerable<AddGuestParameters> addGuestParameters)
        {
            var addGuestTasks = addGuestParameters
                .Select(p => BuildGuest(p.Name, p.MaxExtras))
                .Select(g => _reservationsContext.Guests.AddAsync(g));

            await Task.WhenAll(addGuestTasks);
            await _reservationsContext.SaveChangesAsync();
        }

        public async Task DeleteGuestAsync(string id)
        {
            var guest = _reservationsContext.Guests.First(g => g.Id == id);
            _reservationsContext.Guests.Remove(guest);
            await _reservationsContext.SaveChangesAsync();
        }
        
        public async Task DeleteAllGuestsAsync()
        {
            _reservationsContext.Guests.RemoveRange(_reservationsContext.Guests);
            await _reservationsContext.SaveChangesAsync();
        }

        public async Task UpdateGuestStatusAsync(string guestId, GuestStatus status)
        {
            var guest = await _reservationsContext.Guests.FirstOrDefaultAsync(g => g.Id == guestId);
            GuardAgainstNullGuestTableEntry(guestId, guest);
            guest.Status = status.ToString();

            _reservationsContext.Update(guest);
            await _reservationsContext.SaveChangesAsync();
        }
        
        public async Task UpdateExtrasAsync(string guestId, IEnumerable<string> extras)
        {
            var guest = await _reservationsContext.Guests.FirstOrDefaultAsync(g => g.Id == guestId);
            GuardAgainstNullGuestTableEntry(guestId, guest);

            var newExtras = extras.Select(name => BuildExtra(name, guestId));
            await _reservationsContext.AddRangeAsync(newExtras);
            await _reservationsContext.SaveChangesAsync();
        }

        private static void GuardAgainstNullGuestTableEntry(string identifier, GuestTableEntry guestTableEntry)
        {
            if (guestTableEntry == null)
            {
                throw new NoGuestFoundException(identifier);
            }
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
        
        private static ExtraTableEntry BuildExtra(string name, string guestId)
        {
            return new ExtraTableEntry
            {
                Id = Guid.NewGuid().ToString(),
                GuestTableEntryId = guestId,
                Name = name
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
    
    public class NoGuestFoundException : Exception
    {
        public NoGuestFoundException(string identifier) : base($"No guest found matching [{identifier}].")
        {
            
        }
    }

    public class AddGuestParameters
    {
        public string Name { get; set; }
        public int MaxExtras { get; set; }
    }
}