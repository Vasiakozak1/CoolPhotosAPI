using CoolPhotosAPI.BL.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CoolPhotosAPI.Web.Middlewares
{
    public class CatchExceptionsMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        public CatchExceptionsMiddleware(RequestDelegate requestDelegate)
            => _requestDelegate = requestDelegate;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch(Exception exc)
            {
                await handleExceptionAsync(httpContext, exc);
            }
        }

        private Task handleExceptionAsync(HttpContext httpContext, Exception exc)
        {
            httpContext.Response.ContentType = "application/json";
            BaseException managedException = exc as BaseException;
            if(managedException != null)
            {
                httpContext.Response.StatusCode = (int)managedException.HttpStatusCode;
                return httpContext.Response.WriteAsync(exc.Message);
            }

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return httpContext.Response.WriteAsync(exc.Message);
        }
    }
}
