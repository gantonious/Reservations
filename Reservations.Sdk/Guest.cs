using System.Collections.Generic;

namespace Reservations.Sdk
{
    public class Guest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public IEnumerable<string> Extras { get; set; }
    }
}