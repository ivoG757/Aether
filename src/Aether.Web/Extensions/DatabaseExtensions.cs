using Aether.Data;
using Microsoft.EntityFrameworkCore;

namespace Aether.Web.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection ConfigureDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Add services to the container.
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AetherDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
    }
}
