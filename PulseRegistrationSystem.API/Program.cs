using PulseRegistrationSystem.API.Configuration;
using PulseRegistrationSystem.Application.MapperConfiguration;
using PulseRegistrationSystem.Application.Services.Configuration;
using PulseRegistrationSystem.Domain.SecurityConfiguration;
using PulseRegistrationSystem.Infraestructure.Configuration;
using PulseRegistrationSystem.Infraestructure.Configuration.Secutiry;
using PulseRegistrationSystem.Infrastructure.Persistence.Configs;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCustomVersioning();          
builder.Services.AddCustomSwagger();             
builder.Services.AddCustomHealthChecks(builder.Configuration); 

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();



var settings = builder.Configuration.GetSection("Settings").Get<Settings>();
builder.Services.AddScoped<ISenhaHasher, BCryptSenhaHasher>();
builder.Services.AddInfra(settings);
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddAutoMapper(cfg => {}, typeof(MappingConfig));

var app = builder.Build();


app.UseCustomHealthChecks();

if (app.Environment.IsDevelopment())
{
    app.UseCustomSwagger(); 
}

app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();
app.MapControllers();

app.Run();