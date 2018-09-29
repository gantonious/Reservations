using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reservations.DataServices;
using Reservations.DataServices.Models;

namespace Reservations.WebServices.Controllers
{
    [Route("api/admin/guests")]
    public class AdminController : Controller
    {
        private readonly GuestsService _guestsService;

        public AdminController(GuestsService guestsService)
        {
            _guestsService = guestsService;
        }

        [HttpGet]
        public IEnumerable<Guest> GetGuests()
        {
            return _guestsService.GetAllGuests();
        }

        [HttpPost]
        public async Task AddGuestAsync([FromBody] AddGuestRequest addGuestRequest)
        {
            await _guestsService.AddGuestAsync(addGuestRequest.Name, addGuestRequest.MaxGuests);
        }
    }

    public class AddGuestRequest
    {
        public string Name { get; set; }
        public int MaxGuests { get; set; }
    }
}