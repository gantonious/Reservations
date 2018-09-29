using System.Collections.Generic;

namespace Reservations.DataServices.Models
{
    public class Guest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int MaxExtras { get; set; }
        public GuestStatus Status { get; set; }
        public IEnumerable<string> Extras { get; set; } = new List<string>();
    }
}