using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reservations.DataServices;
using Reservations.DataServices.Models;
using Reservations.WebServices.Models;

namespace Reservations.WebServices.Controllers
{
    [Route("v1/guests")]
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

            return Ok(guest.AsGuestV1());
        }

        [HttpPost("{guestId}/status")]
        public async Task<IActionResult> UpdateGuestStatusAsync(string guestId, [FromBody] UpdateStatusRequest updateStatusRequest)
        {
            try
            {
                if (!Enum.TryParse<GuestStatus>(updateStatusRequest.Status, out var status))
                {
                    return BadRequest($"{updateStatusRequest.Status} is not a valid status.");
                }
              
                await _guestsService.UpdateGuestStatusAsync(guestId, status);
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
        public string Status { get; set; }
    }
    
    public class UpdateExtrasRequest
    {
        public IEnumerable<string> Extras { get; set; }
    }
}