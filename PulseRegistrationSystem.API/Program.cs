using PulseRegistrationSystem.API.Configuration;
using PulseRegistrationSystem.Application.MapperConfiguration;
using PulseRegistrationSystem.Application.Services.Configuration;
using PulseRegistrationSystem.Domain.SecurityConfiguration;
using PulseRegistrationSystem.Infraestructure.Configuration;
using PulseRegistrationSystem.Infraestructure.Configuration.Secutiry;
using PulseRegistrationSystem.Infrastructure.Persistence.Configs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Registration System API",
        Version = "v1",
        Description = "Documentação da API REST do sistema de registro"
    });
});
var settings = builder.Configuration.GetSection("Settings").Get<Settings>();
builder.Services.AddScoped<ISenhaHasher, BCryptSenhaHasher>();
builder.Services.AddInfra(settings);
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddAutoMapper(cfg => {}, typeof(MappingConfig));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomHealthChecks(builder.Configuration);


var app = builder.Build();


app.UseCustomHealthChecks();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

