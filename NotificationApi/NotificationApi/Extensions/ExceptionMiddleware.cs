using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            catch (BadRequestException ex)
            {
                var modelState = new ModelStateDictionary();
                modelState.AddModelError("request", ex.Message);
                var problemDetails = new ValidationProblemDetails(modelState);
                await HandleBadRequestAsync(httpContext, problemDetails);
            }
            catch (NotificationDalException ex)
            {
                var modelState = new ModelStateDictionary();
                modelState.AddModelError("database", ex.Message);
                var problemDetails = new ValidationProblemDetails(modelState);
                await HandleBadRequestAsync(httpContext, problemDetails);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, ex);
            }
        }

        private static Task HandleBadRequestAsync(HttpContext httpContext, ValidationProblemDetails problemDetails)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;

            return httpContext.Response.WriteAsJsonAsync(problemDetails);
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
