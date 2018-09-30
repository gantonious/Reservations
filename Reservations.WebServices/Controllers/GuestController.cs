using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reservations.DataServices;
using Reservations.DataServices.Models;

namespace Reservations.WebServices.Controllers
{
    [Route("api/guests")]
    public class GuestController : Controller
    {
        private readonly GuestsService _guestsService;

        public GuestController(GuestsService guestsService)
        {
            _guestsService = guestsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGuestAsync([FromQuery(Name = "name")] string name)
        {
            var guest = await _guestsService.SearchFor(name);

            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }

        [HttpPost("{guestId}/status")]
        public async Task<IActionResult> UpdateGuestStatusAsync(string guestId, [FromBody] UpdateStatusRequest updateStatusRequest)
        {
            try
            {
                await _guestsService.UpdateGuestStatusAsync(guestId, updateStatusRequest.Status);
                return Ok();
            }
            catch (NoGuestFoundException)
            {
                return NotFound();
                
            }
        }
        
        [HttpPost("{guestId}/extras")]
        public async Task<IActionResult> UpdateGuestExtrasAsync(string guestId, [FromBody] UpdateExtrasRequest updateExtrasRequest)
        {
            try
            {
                await _guestsService.UpdateExtrasAsync(guestId, updateExtrasRequest.Extras);
                return Ok();
            }
            catch (NoGuestFoundException)
            {
                return NotFound();
                
            }
        }
       
    }

    public class UpdateStatusRequest
    {
        public GuestStatus Status { get; set; }
    }
    
    public class UpdateExtrasRequest
    {
        public IEnumerable<string> Extras { get; set; }
    }
}