using System.Collections.Generic;
using System.Threading.Tasks;
using Reservations.Sdk.Rest;

namespace Reservations.Sdk
{
    public class ReservationsClient
    {
        private readonly IAdminApi _adminApi;

        public ReservationsClient(IAdminApi adminApi)
        {
            _adminApi = adminApi;
        }

        public async Task<IEnumerable<Guest>> GetGuestsAsync()
        {
            return await _adminApi.GetGuestsAsync();
        }
        
        public async Task AddGuestAsync(AddGuestRequest addGuestRequest)
        {
            await _adminApi.AddGuestAsync(addGuestRequest);
        }
        
        public async Task AddGuestsAsync(IEnumerable<AddGuestRequest> addGuestRequests)
        {
            await _adminApi.AddGuestsAsync(addGuestRequests);
        }

        public async Task DeleteAllGuestsAsync()
        {
            await _adminApi.DeleteAllGuestsAsync();
        }
    }
}