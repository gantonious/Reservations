namespace Reservations.Database.Models
{
    public class Invitee
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int TotalGuests { get; set; } = 0;
    }
}