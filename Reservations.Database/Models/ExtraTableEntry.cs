using System.Runtime.CompilerServices;

namespace Reservations.Database.Models
{
    public class ExtraTableEntry
    {
        public string Id { get; set; }
        public string GuestTableEntryId { get; set; }
        public string Name { get; set; }
    }
}