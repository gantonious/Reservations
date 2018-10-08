using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Reservations.Database;
using Reservations.DataServices;
using Reservations.WebServices.Middleware;

namespace Reservations.WebServices
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ReservationsContext>(options =>
                {
                    options.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING"));
                });
            services.AddScoped<GuestsService>();
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseTokenProtection(new TokenProtectedMiddleware.Options
            {
                ProtectedPaths = new List<string>
                {
                    "/v1/admin/guests",
                    "/v1/admin/guests/bulk"
                },
                HashedValidTokens = new List<string>()
                {
                    Environment.GetEnvironmentVariable("HASHED_ADMIN_TOKEN")
                }
            });

            app.UseCors(b => 
                b.WithOrigins("https://georgeandjessica.ca")
                 .WithOrigins("http://localhost")
                 .AllowAnyHeader()
                 .AllowAnyMethod()
            );
            
            app.UseMvc();
        }
    }
}