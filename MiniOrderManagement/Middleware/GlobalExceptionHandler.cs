using System.Text.Json;
using MiniOrderManagement.Domain.Exceptions;

namespace MiniOrderManagement.Middleware
{
    /// <summary>
    /// Global exception handler middleware for API error responses
    /// </summary>
    public class GlobalExceptionHandler : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse
            {
                Message = exception.Message,
                Timestamp = DateTime.UtcNow
            };

            return exception switch
            {
                CustomerNotFoundException ex => HandleCustomerNotFound(context, ex, response),
                ArgumentException ex => HandleArgumentException(context, ex, response),
                _ => HandleGenericException(context, exception, response)
            };
        }

        private static Task HandleCustomerNotFound(HttpContext context, CustomerNotFoundException ex, ErrorResponse response)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            response.StatusCode = 404;
            response.Error = "Customer Not Found";
            return context.Response.WriteAsJsonAsync(response);
        }

        private static Task HandleArgumentException(HttpContext context, ArgumentException ex, ErrorResponse response)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            response.StatusCode = 400;
            response.Error = "Invalid Request";
            return context.Response.WriteAsJsonAsync(response);
        }

        private static Task HandleGenericException(HttpContext context, Exception ex, ErrorResponse response)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            response.StatusCode = 500;
            response.Error = "Internal Server Error";
            response.Details = ex.StackTrace;
            return context.Response.WriteAsJsonAsync(response);
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Error { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }
    }
}