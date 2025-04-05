public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    private  string? _validToken ; //Default value

    public AuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _validToken = configuration["Authentication:ValidToken"];
        //validToken ?? throw new ArgumentNullException(nameof(validToken));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized: Missing token.");
            return;
        }

        // Validate the token against the value from appsettings.json
        if (token != _validToken)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized: Invalid token.");
            return;
        }

        await _next(context);
    }
}
