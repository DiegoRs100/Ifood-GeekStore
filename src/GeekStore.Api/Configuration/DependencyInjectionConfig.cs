using GeekStore.Api.BuildingBlocks;
using GeekStore.Business.Interfaces.Repositories;
using GeekStore.Business.Interfaces.Services;
using GeekStore.Business.Services;
using GeekStore.Core.Interfaces.BuildingBlocks;
using GeekStore.Core.Services;
using GeekStore.Data.Contexts;
using GeekStore.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using static GeekStore.Api.Configuration.SwaggerConfig;

namespace GeekStore.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.ResolveContexts();
            services.ResolveRepositories();
            services.ResolveServices();
            services.ResolveBuildingBlocks();

            return services;
        }

        private static IServiceCollection ResolveContexts(this IServiceCollection services)
        {
            services.AddScoped<GeekStoreDbContext>();
            return services;
        }

        private static IServiceCollection ResolveRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            return services;
        }

        private static IServiceCollection ResolveServices(this IServiceCollection services)
        {
            services.AddScoped<IProdutoService, ProdutoService>();
            return services;
        }

        private static IServiceCollection ResolveBuildingBlocks(this IServiceCollection services)
        {
            services.AddScoped<ISessionApp, AppSession>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}