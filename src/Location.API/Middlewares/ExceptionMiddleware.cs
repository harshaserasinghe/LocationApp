using Location.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Location.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Common.Exceptions.ApplicationException exception)
            {
                await HandleApplicationExceptionAsync(httpContext, exception);
            }
        }
        
        private async Task HandleApplicationExceptionAsync(HttpContext httpContext, Common.Exceptions.ApplicationException exception)
        {
            var errorModel = new ErrorModel(exception.ErrorCode, exception.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = exception.ErrorCode;
            await httpContext.Response.WriteAsync(errorModel.ToString());
        }
    }
}
