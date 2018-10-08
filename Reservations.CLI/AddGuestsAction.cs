using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Newtonsoft.Json.Linq;
using Reservations.Sdk;
using Reservations.Sdk.Rest;

namespace Reservations.CLI
{
    public class AddGuestsAction
    {
        private readonly ReservationsClient _reservationsClient;
        private readonly string _guestListCsvFile;
        
        public AddGuestsAction(ReservationsClient reservationsClient, string guestListCsvFile)
        {
            _reservationsClient = reservationsClient;
            _guestListCsvFile = guestListCsvFile;
        }

        public async Task Execute()
        {
            using (var streamReader = new StreamReader(_guestListCsvFile))
            using (var csvReader = new CsvReader(streamReader))
            {
                var guests = csvReader.GetRecords<GuestListItem>().ToList();
                var guestUploadRequests = guests.Select(g => new AddGuestRequest
                {
                    Name = g.Name,
                    MaxExtras = g.MaxExtras
                });

                await _reservationsClient.AddGuestsAsync(guestUploadRequests);
                Console.WriteLine($"Succesfully uploaded {guests.Count()} guests.");
            }
        }
    }

    public class GuestListItem
    {
        public string Name { get; set; }
        public int MaxExtras { get; set; }
    }
}