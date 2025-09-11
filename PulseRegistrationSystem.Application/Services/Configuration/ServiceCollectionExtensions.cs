using Microsoft.Extensions.DependencyInjection;
using PulseRegistrationSystem.Application.Services.Implementation;
using PulseRegistrationSystem.Application.Services.Interface;
using PulseRegistrationSystem.Domain.SecurityConfiguration;
using PulseRegistrationSystem.Infraestructure.Configuration.Secutiry;

namespace PulseRegistrationSystem.Application.Services.Configuration;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ISenhaHasher, BCryptSenhaHasher>();
        return services;
    }
}