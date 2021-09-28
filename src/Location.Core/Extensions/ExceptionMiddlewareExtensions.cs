using Location.Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Location.Core.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {       
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}