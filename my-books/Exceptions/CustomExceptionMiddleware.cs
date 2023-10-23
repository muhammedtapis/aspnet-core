using Microsoft.AspNetCore.Http;
using my_books.Data.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace my_books.Exceptions
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next) //inject requestdelegate
        {
            _next = next;          
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (System.Exception ex)
            {

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            //construct the error response here

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            var response = new ErrorVM()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = "Internal Server Error From the Custom Middleware",
                Path = "path-goes-here"
            };

            return httpContext.Response.WriteAsync(response.ToString()); 
        }
    }
}
