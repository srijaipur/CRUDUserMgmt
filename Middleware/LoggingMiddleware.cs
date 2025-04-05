public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log request details
        _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path}");

        await _next(context); // Proceed to next middleware

        // Log response details
        _logger.LogInformation($"Response: {context.Response.StatusCode}");
    }
}
