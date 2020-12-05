﻿using GeekStore.Api.BuildingBlocks.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Api.Configurations
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            //app.UseMiddleware<SwaggerAuthorizedMiddleware>();
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            });

            return app;
        }

        #region Classes

        public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            #region Injection

            private readonly IApiVersionDescriptionProvider provider;
            private readonly SwaggerSettings _swaggerSettings;

            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<SwaggerSettings> swaggerSettings)
            {
                this.provider = provider;
                _swaggerSettings = swaggerSettings.Value;
            }

            #endregion

            public void Configure(SwaggerGenOptions options)
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }

            private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
            {
                var info = new OpenApiInfo()
                {
                    Title = _swaggerSettings.Title,
                    Version = description.ApiVersion.ToString(),
                    Description = _swaggerSettings.Description,
                    Contact = new OpenApiContact() { Name = _swaggerSettings.ContactName, Email = _swaggerSettings.ContactEmail },
                    License = new OpenApiLicense() { Name = _swaggerSettings.LicenseName, Url = new Uri(_swaggerSettings.LicenseUrl) }
                };

                if (description.IsDeprecated)
                    info.Description += " (Esta versão está obsoleta!)";

                return info;
            }
        }

        public class SwaggerAuthorizedMiddleware
        {
            #region Injection

            private readonly RequestDelegate _next;

            public SwaggerAuthorizedMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            #endregion

            public async Task Invoke(HttpContext context)
            {
                if (context.Request.Path.StartsWithSegments("/swagger") && !context.User.Identity.IsAuthenticated)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }

                await _next.Invoke(context);
            }
        }

        public class SwaggerDefaultValues : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                    return;

                foreach (var parameter in operation.Parameters)
                {
                    var description = context.ApiDescription
                        .ParameterDescriptions
                        .First(p => p.Name == parameter.Name);

                    var routeInfo = description.RouteInfo;

                    operation.Deprecated = OpenApiOperation.DeprecatedDefault;

                    if (parameter.Description == null)
                        parameter.Description = description.ModelMetadata?.Description;

                    if (routeInfo == null)
                        continue;

                    if (parameter.In != ParameterLocation.Path && parameter.Schema.Default == null)
                        parameter.Schema.Default = new OpenApiString(routeInfo.DefaultValue.ToString());

                    parameter.Required |= !routeInfo.IsOptional;
                }
            }
        }

        #endregion
    }
}