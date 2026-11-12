using Aether.Data;
using Aether.Data.Models;
using Aether.GCommon.Validation;

namespace Aether.Web.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection ConfigureIdentity(
            this IServiceCollection services, 
            IConfiguration configuration)
        { //TODO: Put into configuration
            services.AddDefaultIdentity<ApplicationUser>(options => {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequiredLength = UserValidationConstants.PasswordMinLength;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<AetherDbContext>();

            return services;
        }
    }
}
