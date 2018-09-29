using System.Runtime.CompilerServices;

namespace Reservations.Database.Models
{
    public class Guest
    {
        public string Id { get; set; }
        public string InviteeId { get; set; }
        public string Name { get; set; }
    }
}