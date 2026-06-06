namespace ECommerceAdminPortal.Middleware;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder
        UseRequestLogging(
            this IApplicationBuilder app)
    {
        return app.UseMiddleware<
            RequestLoggingMiddleware>();
    }

    public static IApplicationBuilder
    UseGlobalException(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<
            GlobalExceptionMiddleware>();
    }
}