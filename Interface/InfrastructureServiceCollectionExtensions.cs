using Infrastructure.Contracts;
using Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MonkeyShelterDbContext>(options =>
            options.UseSqlite(connectionString));

            services.AddTransient<IAdmissionsRepository, AdmissionsRepository>();

            services.AddTransient<IMonkeyRepository, MonkeyRepository>();

            services.AddScoped<InitialDbDataSeed>();


            return services;


        }
    }
}
