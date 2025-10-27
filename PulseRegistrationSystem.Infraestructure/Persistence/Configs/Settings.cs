using PulseRegistrationSystem.Infraestructure.Configuration;

namespace PulseRegistrationSystem.Infrastructure.Persistence.Configs;

public class Settings
{
    public MongoDbSettings MongoDb { get; set; } = new MongoDbSettings();
}