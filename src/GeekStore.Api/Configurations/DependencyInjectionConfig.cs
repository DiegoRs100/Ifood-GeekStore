using GeekStore.Api.BuildingBlocks.Extentions;
using GeekStore.Api.BuildingBlocks;
using GeekStore.Api.BuildingBlocks.Settings;
using GeekStore.Business.Interfaces.Repositories;
using GeekStore.Business.Interfaces.Services;
using GeekStore.Business.Services;
using GeekStore.Core.Facades;
using GeekStore.Core.Interfaces.BuildingBlocks;
using GeekStore.Core.Services;
using GeekStore.Data.Contexts;
using GeekStore.Data.Repository;
using GeekStore.Core.Settings;
using GeekStore.Core.Interfaces.Repositories;
using GeekStore.Core.Interfaces.Services;
using GeekStore.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using Polly;
using static GeekStore.Api.Configurations.SwaggerConfig;

namespace GeekStore.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.ResolveContexts();
            services.ResolveRepositories();
            services.ResolveServices();
            services.ResolveHttpServices();
            services.ResolveSettings();
            services.ResolveBuildingBlocks();

            return services;
        }

        private static IServiceCollection ResolveContexts(this IServiceCollection services)
        {
            services.AddScoped<IGeekStoreDbContextService, GeekStoreDbContextService>();
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

        private static IServiceCollection ResolveHttpServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticationFacade, AuthenticationFacade>()
                .AddPolicyHandler(PollyExtensions.EsperarTentar())
                .AddTransientHttpErrorPolicy(x =>
                    x.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

            return services;
        }

        public static IServiceCollection ResolveSettings(this IServiceCollection services)
        {
            services.AddScoped<SwaggerSettings>();
            services.AddScoped<ApiAuthenticationSettings>();
            services.AddScoped<AppSettings>();
            services.AddScoped<FileServerSettings>();

            return services;
        }

        private static IServiceCollection ResolveBuildingBlocks(this IServiceCollection services)
        {
            services.AddScoped<IImagemRepository, ImagemRepository>();

            services.AddScoped<IFileServerService, FileServerService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ISessionApp, AppSession>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return services;
        }
    }
}