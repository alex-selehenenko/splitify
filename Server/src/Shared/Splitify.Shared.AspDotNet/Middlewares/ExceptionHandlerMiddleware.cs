using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Splitify.Shared.AspDotNet.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Internal server error");
                context.Response.StatusCode = 500;

                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}
