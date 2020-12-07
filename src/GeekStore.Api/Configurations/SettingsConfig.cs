using GeekStore.Api.BuildingBlocks.Settings;
using GeekStore.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeekStore.Api.Configurations
{
    public static class SettingsConfig
    {
        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            #region Swagger

            var swaggerSettings = configuration.GetSection("SwaggerSettings");
            services.Configure<SwaggerSettings>(swaggerSettings);

            #endregion

            #region API - Authentication

            var apiAuthenticationSettings = configuration.GetSection("ApiAuthenticationSettings");
            services.Configure<ApiAuthenticationSettings>(apiAuthenticationSettings);

            #endregion

            #region FileServer

            var fileServerSettings = configuration.GetSection("FileServerSettings");
            services.Configure<FileServerSettings>(fileServerSettings);

            #endregion

            #region Application Settings

            var appSettings = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);

            #endregion

            return services;
        }
    }
}