using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;

namespace Reservations.Sdk.Rest
{
    public interface IAdminApi
    {
        [Get("/v1/admin/guests")]
        Task<IEnumerable<Guest>> GetGuestsAsync();

        [Post("/v1/admin/guests")]
        Task AddGuestAsync([Body] AddGuestRequest addGuestRequest);

        [Post("/v1/admin/guests/bulk")]
        Task AddGuestsAsync([Body] IEnumerable<AddGuestRequest> addGuestRequests);
        
        [Delete("/v1/admin/guests/bulk")]
        Task DeleteAllGuestsAsync();
    }
}