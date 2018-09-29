namespace Reservations.Database.Models
 {
     public class Guest
     {
         public string Id { get; set; }
         public string Name { get; set; }
         public string Status { get; set; }
         public int TotalGuests { get; set; } = 0;
     }
 }