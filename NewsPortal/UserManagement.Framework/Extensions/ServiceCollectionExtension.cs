using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement.Framework.Context;
using UserManagement.Framework.Interfaces;
using UserManagement.Framework.Services;
using UserManagement.Framework.Repositories;

namespace UserManagement.Framework.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddUserManagementFramework(this IServiceCollection services, IConfiguration configuration)
        {
            //Connection String
            services.AddDbContext<UserManagementContext>(options => options.UseSqlServer(configuration.GetConnectionString("UserDb"), providerOptions => providerOptions.EnableRetryOnFailure()));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserManagementUnitOfWork, UserManagementUnitOfWork>();

            return services;
        }
    }
}
