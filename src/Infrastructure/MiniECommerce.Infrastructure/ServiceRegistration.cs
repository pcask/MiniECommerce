using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Application.Abstractions.Storage;
using MiniECommerce.Infrastructure.Services;
using MiniECommerce.Infrastructure.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : StorageBase, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
