using Microsoft.Extensions.DependencyInjection;
using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Infraestructure.Repositories.Implementation;
using PulseRegistrationSystem.Infraestructure.Repositories.Interface;
using PulseRegistrationSystem.Infrastructure.Persistence.Configs;
using PulseRegistrationSystem.Infrastructure.Persistence.Database;

namespace PulseRegistrationSystem.Infraestructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfra(this IServiceCollection services, Settings settings)
    {
        services.AddSingleton(settings);      
        services.AddScoped<MongoDbContext>();    
        services.AddScoped<ILoginRepository, LoginRepository>(); 
        services.AddScoped<IMethodsRepository<Usuario>, UsuarioRepository>(); 
        return services;
    }
}