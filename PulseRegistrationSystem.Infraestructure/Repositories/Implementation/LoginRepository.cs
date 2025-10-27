using MongoDB.Driver;
using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Infraestructure.Repositories.Interface;
using PulseRegistrationSystem.Infrastructure.Persistence.Database;

namespace PulseRegistrationSystem.Infraestructure.Repositories.Implementation;

public class LoginRepository : ILoginRepository
{
    private readonly IMongoCollection<Login> _logins;

    public LoginRepository(MongoDbContext context)
    {
        _logins = context.GetCollection<Login>("logins");
    }

    public async Task<IEnumerable<Login>> GetAllAsync() =>
        await _logins.Find(_ => true).ToListAsync();

    public async Task<Login?> GetByIdAsync(string id) =>
        await _logins.Find(l => l.Id == id).FirstOrDefaultAsync();

    public async Task AddAsync(Login entity) =>
        await _logins.InsertOneAsync(entity);

    public async Task UpdateAsync(Login entity)
    {
        var filter = Builders<Login>.Filter.Eq(l => l.Id, entity.Id);
        await _logins.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveAsync(Login entity)
    {
        var filter = Builders<Login>.Filter.Eq(l => l.Id, entity.Id);
        await _logins.DeleteOneAsync(filter);
    }

    public async Task<Login?> GetByCpfAsync(string cpf) =>
        await _logins.Find(l => l.NumeroCpf == cpf).FirstOrDefaultAsync();
}