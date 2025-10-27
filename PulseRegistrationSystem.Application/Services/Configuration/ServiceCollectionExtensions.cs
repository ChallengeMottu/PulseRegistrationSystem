using Microsoft.Extensions.DependencyInjection;
using PulseRegistrationSystem.Application.Services.Implementation;
using PulseRegistrationSystem.Application.Services.Interface;

namespace PulseRegistrationSystem.Application.Services.Configuration;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<ILoginService, LoginService>();
        return services;
    }
    
}