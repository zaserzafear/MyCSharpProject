using Database.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataBaseLayer(this IServiceCollection services, string connectionString)
        {
            AddDbContext<AppDbContext>(services, connectionString);

            return services;
        }

        private static void AddDbContext<TContext>(IServiceCollection services, string connectionString) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            {
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
        }
    }
}
