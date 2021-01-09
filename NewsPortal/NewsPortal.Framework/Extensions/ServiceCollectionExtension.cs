using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            //services.AddDbContext<SpartaActiveContext>(options => options.UseSqlServer(configuration.GetConnectionString("SpartaActiveDb"), providerOptions => providerOptions.EnableRetryOnFailure()), ServiceLifetime.Transient);

            //// Services
            //services.AddScoped<IDetectionService, DetectionService>();
            //services.AddScoped<IDashboardService, DashboardService>();
            //services.AddScoped<ICameraService, CameraService>();
            //services.AddScoped<IMatchService, MatchService>();
            //services.AddScoped<ILocationService, LocationService>();

            //// Repository
            //services.AddScoped<ISpartaActiveUnitOfWork, SpartaActiveUnitOfWork>();

            return services;
        }
    }
}
