using System.Collections.Generic;
using Reservations.DataServices.Models;

namespace Reservations.WebServices.Models
{
    public class GuestV1
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int MaxExtras { get; set; }
        public string Status { get; set; }
        public IEnumerable<string> Extras { get; set; } = new List<string>();
    }

    public static class GuestExtensions
    {
        public static GuestV1 AsGuestV1(this Guest guest)
        {
            return new GuestV1
            {
                Id = guest.Id,
                Name = guest.Name,
                MaxExtras = guest.MaxExtras,
                Status = guest.Status.ToString(),
                Extras = guest.Extras
            };
        }
    }
}