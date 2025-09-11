using Microsoft.EntityFrameworkCore;
using PulseRegistrationSystem.Application.MapperConfiguration;
using PulseRegistrationSystem.Application.Services.Configuration;
using PulseRegistrationSystem.Infraestructure.Configuration;
using PulseRegistrationSystem.Infraestructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

builder.Services.AddControllers();
builder.Services.AddAppDbContext(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddAutoMapper(cfg => {}, typeof(MappingConfig));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();


app.Run();

