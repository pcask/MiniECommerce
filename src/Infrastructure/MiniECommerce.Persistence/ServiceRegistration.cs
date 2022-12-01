using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Application.Abstractions.Services;
using MiniECommerce.Application.Abstractions.Services.Authentication;
using MiniECommerce.Application.Repositories.NCustomer;
using MiniECommerce.Application.Repositories.NFile;
using MiniECommerce.Application.Repositories.NInvoiceFile;
using MiniECommerce.Application.Repositories.NOrder;
using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Application.Repositories.NProductImageFile;
using MiniECommerce.Domain.Entities.Identity;
using MiniECommerce.Persistence.Contexts;
using MiniECommerce.Persistence.Repositories.NCustomer;
using MiniECommerce.Persistence.Repositories.NFile;
using MiniECommerce.Persistence.Repositories.NInvoiceFile;
using MiniECommerce.Persistence.Repositories.NOrder;
using MiniECommerce.Persistence.Repositories.NProduct;
using MiniECommerce.Persistence.Repositories.NProductImageFile;
using MiniECommerce.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Persistence
{
    public static class ServiceRegistration
    {
        public static void ConfigureNpgSql(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MiniECommerceDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Npgsql"),
                optionBuilder => optionBuilder.MigrationsAssembly("MiniECommerce.Persistence")));
        }

        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<MiniECommerceDbContext>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();
            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
        }
    }
}
