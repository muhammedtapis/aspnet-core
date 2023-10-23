﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using my_books.Data.ViewModels;
using System.Net;

namespace my_books.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureBuildInExceptionHandler(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";  

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var contextRequest = context.Features.Get<IHttpRequestFeature>();

                    if(contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorVM()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,
                            Path =contextRequest.Path
                        }.ToString());
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionHandler (this IApplicationBuilder appBuilder)
        {
            appBuilder.UseMiddleware<CustomExceptionMiddleware>();  //custom kullanabilmek için.
        }
    }
}
