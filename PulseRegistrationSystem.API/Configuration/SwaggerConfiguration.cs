using Microsoft.OpenApi.Models;
using System.Reflection;

namespace PulseRegistrationSystem.API.Configuration
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                
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

                
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Registration System API V2",
                    Version = "v2",
                    Description = "Documentação da API REST do sistema de registro - Versão 2.0 (com autenticação JWT)",
                    Contact = new OpenApiContact
                    {
                        Name = "Gabriela Sousa Reis",
                        Email = "gabriela@email.com"
                    }
                });

                
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });

                
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
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
}
