using Application.Auth.Implementations;
using Application.Contracts;
using Application.Contracts.Auth;
using Application.Contracts.Business;
using Application.Implementations;
using Application.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMemoryCache();

            services.AddTransient<IAdmissionTracker, AdmissionsTracker>();

            services.AddTransient<IMonkeyService, MonkeyService>();

            services.AddTransient<ICheckupService, CheckupService>();

            services.AddTransient<IDepartureService, DepartureService>();

            services.AddTransient<IShelterService, ShelterService>();

            services.AddTransient<IShelterAuthorizationService, ShelterAuthorizationService>();

            var jwtSettings = new JwtSettings();
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddScoped<IJwtService, JwtService>();


            return services;
        }
    }
}
