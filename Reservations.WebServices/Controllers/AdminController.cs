﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Reservations.DataServices;
using Reservations.DataServices.Models;
using Reservations.WebServices.Models;

namespace Reservations.WebServices.Controllers
{
    [Route("v1/admin")]
    public class AdminController : Controller
    {
        private readonly GuestsService _guestsService;

        public AdminController(GuestsService guestsService)
        {
            _guestsService = guestsService;
        }

        [HttpGet("guests")]
        public IEnumerable<GuestV1> GetGuests()
        {
            return _guestsService.GetAllGuests().Select(g => g.AsGuestV1());
        }

        [HttpPost("guests")]
        public async Task AddGuestAsync([FromBody] AddGuestRequest addGuestRequest)
        {
            await _guestsService.AddGuestAsync(new AddGuestParameters
            {
                Name = addGuestRequest.Name,
                MaxExtras = addGuestRequest.MaxGuests
            });
        }
        
        [HttpPost("guests/bulk")]
        public async Task AddGuestsAsync([FromBody] IEnumerable<AddGuestRequest> addGuestRequests)
        {
            await _guestsService.AddGuestsAsync(addGuestRequests.Select(r => new AddGuestParameters
            {
                Name = r.Name,
                MaxExtras = r.MaxGuests
            }));
        }

        [HttpDelete("guests/bulk")]
        public async Task DeleteGuestsAsync()
        {
            await _guestsService.DeleteAllGuestsAsync();
        }

        [HttpGet("stats")]
        public async Task<Stats> GetStatsAsync()
        {
            return await _guestsService.GetStatsAsync();
        }
        
    }

    public class AddGuestRequest
    {
        public string Name { get; set; }
        public int MaxGuests { get; set; }
    }
}