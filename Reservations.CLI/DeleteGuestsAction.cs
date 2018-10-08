using System.Threading.Tasks;
using Reservations.Sdk;

namespace Reservations.CLI
{
    public class DeleteGuestsAction
    {
        private readonly ReservationsClient _reservationsClient;
        
        public DeleteGuestsAction(ReservationsClient reservationsClient)
        {
            _reservationsClient = reservationsClient;
        }

        public async Task Execute()
        {
            await _reservationsClient.DeleteAllGuestsAsync();
        }
    }
}