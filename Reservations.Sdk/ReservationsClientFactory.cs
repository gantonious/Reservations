using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using Reservations.Sdk.Rest;

namespace Reservations.Sdk
{
    public static class ReservationsClientFactory
    {
        public static ReservationsClient BuildClient(ReservationsConfig config)
        {
            var adminApi = RestService.For<IAdminApi>(BuildAuthenticatingHttpClientFrom(config));
            return new ReservationsClient(adminApi);
        }
        
        private static HttpClient BuildAuthenticatingHttpClientFrom(ReservationsConfig config)
        {
            return new HttpClient(new AuthenticatingHttpClientHandler(config.AuthenticationToken))
            {
                BaseAddress = new Uri(config.BaseUrl)
            };
        }
    }
    
    public class AuthenticatingHttpClientHandler : HttpClientHandler
    {
        private readonly string _apiToken;

        public AuthenticatingHttpClientHandler(string apiToken)
        {
            _apiToken = apiToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", _apiToken);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}