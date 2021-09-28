using Location.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Location.Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Common.Exceptions.ApplicationException exception)
            {
                await HandleApplicationExceptionAsync(httpContext, exception);
            }
        }
        
        private async Task HandleApplicationExceptionAsync(HttpContext httpContext, Common.Exceptions.ApplicationException exception)
        {
            var errorModel = new ErrorModel(exception.ErrorCode, exception.Message);
            logger.LogWarning(errorModel.ToString());
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = exception.ErrorCode;
            await httpContext.Response.WriteAsync(errorModel.ToString());
        }
    }
}
