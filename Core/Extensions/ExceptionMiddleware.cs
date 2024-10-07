using Core.Constants;
using Core.Exceptions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

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
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "Internal Server Error";

            if (e is ValidationException validationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = validationException.Message;
                return httpContext.Response.WriteAsync(new ValidationErrorDetails
                {
                    StatusCode = statusCode,
                    Message = message,
                    ValidationErrors = validationException.Errors
                }.ToString());
            }


            if (e is AuthorizationException authorizationException) 
            {
                statusCode = (int)HttpStatusCode.Forbidden; 
                message = authorizationException.Message;
            }

            httpContext.Response.StatusCode = statusCode;
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = statusCode,
                Message = message
            }.ToString());
        }



    }
}
