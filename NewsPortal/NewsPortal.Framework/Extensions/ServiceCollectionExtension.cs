using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsPortal.Framework.Data.Context;
using NewsPortal.Framework.Interfaces;
using NewsPortal.Framework.Repository;
using NewsPortal.Framework.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Framework.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddSpartaActiveFramework(this IServiceCollection services, IConfiguration configuration)
        {
            //Connection String
            services.AddDbContext<NewsPortalContext>(options => options.UseSqlServer(configuration.GetConnectionString("NewsPortalDb"), providerOptions => providerOptions.EnableRetryOnFailure()));

            //// Services
            services.AddScoped<INewsPortalService, NewsPortalService>();


            //// Repository
            services.AddScoped<INewsPortalUnitOfWork, NewsPortalUnitOfWork>();

            return services;
        }
    }
}
