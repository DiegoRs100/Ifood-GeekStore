using GeekStore.Core.Dto_s;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeekStore.Api.BuildingBlocks
{
    public class ExceptionMiddleware
    {
        #region Injection

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var apiResponse = new ApiResponse<object>(new Notificacao(exception.Message, exception.InnerException?.Message));

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}