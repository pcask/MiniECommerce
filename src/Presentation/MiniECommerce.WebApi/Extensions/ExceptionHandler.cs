using Microsoft.AspNetCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.WebApi.Extensions
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler<T>(this WebApplication application, ILogger<T> logger)
        {
            application.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;

                    var features = context.Features.Get<IExceptionHandlerFeature>();
                    if (features != null)
                    {
                        logger.LogError(features.Error.Message);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(new
                        {
                            StaatusCode = context.Response.StatusCode,
                            Message = features.Error.Message
                        }));
                    }
                });
            });
        }
    }
}
