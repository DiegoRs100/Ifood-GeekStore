using GeekStore.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Api.Configuration
{
    public static class ContextsConfig
    {
        public static IServiceCollection AddContextsConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GeekStoreDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("GeekStoreSqlConnection")));

            return services;
        }
    }
}