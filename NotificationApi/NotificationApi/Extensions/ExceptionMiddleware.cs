using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NotificationApi.Common;
using NotificationApi.DAL.Exceptions;

namespace NotificationApi.Extensions
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
            catch(Exception ex) when( ex is NotificationDalException || ex is BadRequestException)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.BadRequest, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) statusCode;
            var sb = new StringBuilder(exception.Message);
            var innerException = exception.InnerException;
            while (innerException != null)
            {
                sb.Append($" {innerException.Message}");
                innerException = innerException.InnerException;
            }
            return context.Response.WriteAsJsonAsync(sb.ToString());
        }
    }
}
