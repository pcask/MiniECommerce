﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniECommerce.Application.Repositories.NCustomer;
using MiniECommerce.Application.Repositories.NOrder;
using MiniECommerce.Application.Repositories.NProduct;
using MiniECommerce.Persistence.Contexts;
using MiniECommerce.Persistence.Repositories.NCustomer;
using MiniECommerce.Persistence.Repositories.NOrder;
using MiniECommerce.Persistence.Repositories.NProduct;
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

        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}
