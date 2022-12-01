using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Application.Abstractions.GoogleIdToken;
using MiniECommerce.Application.Abstractions.Token;
using MiniECommerce.Application.Abstractions.Storage;
using MiniECommerce.Infrastructure.Services;
using MiniECommerce.Infrastructure.Services.GoogleIdToken;
using MiniECommerce.Infrastructure.Services.Token;
using MiniECommerce.Infrastructure.Services.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniECommerce.Application.Abstractions.FacebookAuthToken;
using MiniECommerce.Infrastructure.Services.FacebookAuthToken;

namespace MiniECommerce.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IGoogleIdTokenValidationService, GoogleIdTokenValidationService>();
            services.AddScoped<IFacebookAuthTokenValidationService, FacebookAuthTokenValidationService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : StorageBase, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
