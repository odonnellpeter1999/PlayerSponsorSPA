using Microsoft.AspNetCore.Identity;
using PlayerSponsor.Server.Data.Context;
using PlayerSponsor.Server.Models;

namespace PlayerSponsor.Server.Auth
{
    public static class IdentityServiceHelper
    {
        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddApiEndpoints();

            return services;
        }
    }
}
