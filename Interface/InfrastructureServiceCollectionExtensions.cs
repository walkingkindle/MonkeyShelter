using Application.Contracts.Repositories;
using Infrastructure.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<MonkeyShelterDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("SQLLite")));

            services.AddTransient<IAdmissionsRepository, AdmissionsRepository>();

            services.AddTransient<IMonkeyRepository, MonkeyRepository>();

            services.AddTransient<IDeparturesRepository, DeparturesRepository>();

            services.AddTransient<IShelterRepository, ShelterRepository>();

            services.AddTransient<IShelterManagerRepository, ShelterManagerRepository>();

            services.AddScoped<InitialDbDataSeed>();




            return services;


        }
    }
}
