using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Reservations.WebServices.Middleware
{
    public class TokenProtectedMiddleware
    {
        
        private readonly RequestDelegate _next;
        private readonly Options _options;
 
        public TokenProtectedMiddleware(RequestDelegate next, Options options)
        {
            _next = next;
            _options = options;
        } 
        
        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.ToString();

            // endpoint is not protected so we can go ahead and not use it
            if (!_options.ProtectedPaths.Any(p => path.Contains(p, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
                return;
            }
            
            var authHeader = context.Request.Headers["Authorization"];
            var hashOfAuthHeader = GetSha256HashOf(authHeader);
                
            if (_options.HashedValidTokens.Any(t => hashOfAuthHeader.Equals(t, StringComparison.OrdinalIgnoreCase)))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
            }
        }

        private static string GetSha256HashOf(string str)
        {
            using (var hash = SHA256.Create()) {
                return String.Concat(hash
                    .ComputeHash(Encoding.UTF8.GetBytes(str))
                    .Select(item => item.ToString("x2")));
            }
        }
        
        public class Options
        {
            public IEnumerable<string> ProtectedPaths { get; set; }
            public IEnumerable<string> HashedValidTokens { get; set; }
        }
    }
}