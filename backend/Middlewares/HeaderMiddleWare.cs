namespace backend.Middlewares;
class SecureHeadersMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("X-XSS-Protection", "0");
        context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
        context.Response.Headers.Append("X-Frame-Options", "deny");
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; frame-ancestors 'none';");
        context.Response.Headers.Append("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
        context.Response.Headers.Append("Pragma", "no-cache");

        await _next(context);
    }
}

public static class SecureHeadersMiddlewareExtensions
{
    public static IApplicationBuilder UseSecureHeaders(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SecureHeadersMiddleware>();
    }
}
