using Core.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExceptionHandler _httpExceptionHandler = new();
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _contextAccessor = contextAccessor;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await LogException(context, exception);
                await HandleExceptionAsync(context.Response, exception);
            }
        }

        private Task HandleExceptionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }

        private Task LogException(HttpContext context, Exception exception)
        {
            _logger.LogError("ExceptionMiddleware", exception);
            return Task.CompletedTask;
        }
    }
}
