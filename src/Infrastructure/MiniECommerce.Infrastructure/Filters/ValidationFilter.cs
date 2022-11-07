using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(m => m.Value.Errors.Any())
                    .ToDictionary(m => m.Key, m => m.Value.Errors.Select(e => e.ErrorMessage))
                    .ToArray();

                context.Result = new BadRequestObjectResult(errors); // Client'a hataları gönderiyoruz.
                return; // Eğer modelState valid değilse bir sonraki filter'a geçilmeyecektir.
            }

            await next(); // Eğer modelState valid ise bir sonraki filter'ı tetikliyoruz.
        }
    }
}
