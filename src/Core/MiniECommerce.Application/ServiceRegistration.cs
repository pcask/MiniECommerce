using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // IMediatR ve Application katmanındaki tüm IRequest, IRequestHandler interfacelerine karşılık gelen nesneleri IoC Container'a kaydediyoruz.
            services.AddMediatR(typeof(ServiceRegistration));
        }
    }
}
