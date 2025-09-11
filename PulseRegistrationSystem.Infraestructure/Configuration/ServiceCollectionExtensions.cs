using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Domain.SecurityConfiguration;
using PulseRegistrationSystem.Infraestructure.Configuration.Secutiry;
using PulseRegistrationSystem.Infraestructure.Persistence;
using PulseRegistrationSystem.Infraestructure.Repositories.Implementation;
using PulseRegistrationSystem.Infraestructure.Repositories.Interface;

namespace PulseRegistrationSystem.Infraestructure.Configuration;

public static class ServiceCollectionExtensions

{

    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)

    {

        services.AddDbContext<PulseRegistrationSystemContext>(options =>

            options.UseOracle(configuration.GetConnectionString("RegistrationSystem")));

        return services;

    }
 
    public static IServiceCollection AddRepositories(this IServiceCollection services)

    {

        services.AddScoped<ILoginRepository, LoginRepository>();

        services.AddScoped<IMethodsRepository<Usuario>, UsuarioRepository>();

        return services;

    }
 
    public static IServiceCollection AddServices(this IServiceCollection services)

    {

        services.AddScoped<ILoginService, LoginService>();

        services.AddScoped<IUsuarioService, CadastroService>();

        services.AddScoped<ISenhaHasher, BCryptSenhaHasher>();

        return services;

    }

}

