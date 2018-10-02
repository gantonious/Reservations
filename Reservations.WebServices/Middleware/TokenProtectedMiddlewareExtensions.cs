using Microsoft.AspNetCore.Builder;

namespace Reservations.WebServices.Middleware
{
    public static class TokenProtectedMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenProtection(this IApplicationBuilder applicationBuilder, TokenProtectedMiddleware.Options options)
        {
            return applicationBuilder.UseMiddleware<TokenProtectedMiddleware>(options);
        }
    }
}