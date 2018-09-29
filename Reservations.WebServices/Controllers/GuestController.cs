using Microsoft.AspNetCore.Mvc;
using Reservations.DataServices;

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
    }
}