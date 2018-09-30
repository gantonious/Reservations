namespace Reservations.DataServices.Models
{
    public class Stats
    {
        public int TotalUnresponsive { get; set; }
        public int TotalAttending { get; set; }
        public int TotalExtras { get; set; }
        public int TotalNotAttending { get; set; }
    }
}