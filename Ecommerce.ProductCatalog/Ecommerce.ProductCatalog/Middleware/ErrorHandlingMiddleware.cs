using Serilog;

namespace Ecommerce.ProductCatalog.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Log.Error($"An error occurred: {ex.Message}");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An internal server error occurred.");
        }
    }
}