using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text;
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
            var error = Encoding.UTF8.GetBytes($"Exception: {exception.Message} | InnerException: {exception.InnerException.Message}");
            await context.Response.Body.WriteAsync(error, 0, error.Length);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}