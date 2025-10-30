using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MongoDB.Driver;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace PulseRegistrationSystem.API.Configuration
{
    public static class HealthCheckConfig
    {
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoSettings = configuration.GetSection("Settings:MongoDb");
            var connectionString = mongoSettings.GetValue<string>("ConnectionString");
            var databaseName = mongoSettings.GetValue<string>("DatabaseName");

            services.AddHealthChecks()
                .AddMongoDb(
                    sp => new MongoClient(connectionString).GetDatabase(databaseName),
                    name: "MongoDB",
                    timeout: TimeSpan.FromSeconds(5),
                    tags: new[] { "db", "mongo" },
                    failureStatus: HealthStatus.Unhealthy 
                )
                
                .AddCheck(
                    "API",
                    () => HealthCheckResult.Healthy("API rodando!"),
                    tags: new[] { "api" } 
                );

            return services;
        }

        public static IApplicationBuilder UseCustomHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,

                
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            });

            return app;
        }
    }
}
