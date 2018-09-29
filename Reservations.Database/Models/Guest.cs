using System.ComponentModel.DataAnnotations;

namespace Reservations.Database.Models
 {
     public class Guest
     {
         public string Id { get; set; }
         public string Name { get; set; }
         public string Status { get; set; }
         public int TotalExtras { get; set; } = 0;
     }
 }