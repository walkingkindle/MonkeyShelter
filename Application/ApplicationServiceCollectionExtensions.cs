using Application.Contracts;
using Application.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddMemoryCache();

            services.AddTransient<IAdmissionTracker, AdmissionsTracker>();

            services.AddTransient<IMonkeyService, MonkeyService>();

            services.AddTransient<ICheckupService, CheckupService>();

            services.AddTransient<IDepartureService, DepartureService>();

            return services;
        }
    }
}
