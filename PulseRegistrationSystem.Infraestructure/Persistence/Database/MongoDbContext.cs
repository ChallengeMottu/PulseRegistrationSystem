using MongoDB.Driver;
using PulseRegistrationSystem.Infrastructure.Persistence.Configs;

namespace PulseRegistrationSystem.Infrastructure.Persistence.Database;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(Settings settings)
    {
        if (settings?.MongoDb == null)
            throw new ArgumentNullException(nameof(settings), "MongoDbSettings não está configurado.");

        var client = new MongoClient(settings.MongoDb.ConnectionString);
        _database = client.GetDatabase(settings.MongoDb.DatabaseName);
    }

    public IMongoDatabase Database => _database;

    // Método auxiliar para pegar coleções
    public IMongoCollection<T> GetCollection<T>(string name)
        => _database.GetCollection<T>(name);
}