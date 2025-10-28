using Microsoft.OpenApi.Models;

namespace PulseRegistrationSystem.API.Configuration;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            // 🔹 Documento para V1
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Registration System API V1",
                Version = "v1",
                Description = "Documentação da API REST do sistema de registro - Versão 1.0",
                Contact = new OpenApiContact
                {
                    Name = "Gabriela Sousa Reis",
                    Email = "gabriela@email.com"
                }
            });
            
            // 🔹 Documento para V2
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "Registration System API V2", 
                Version = "v2",
                Description = "Documentação da API REST do sistema de registro - Versão 2.0",
                Contact = new OpenApiContact
                {
                    Name = "Gabriela Sousa Reis", 
                    Email = "gabriela@email.com"
                }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Registration API V1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "Registration API V2");
        });

        return app;
    }
}