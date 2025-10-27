using MongoDB.Driver;
using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Infraestructure.Repositories.Interface;
using PulseRegistrationSystem.Infrastructure.Persistence.Database;


public class UsuarioRepository : IMethodsRepository<Usuario>
{
    private readonly IMongoCollection<Usuario> _usuarios;

    public UsuarioRepository(MongoDbContext context)
    {
        _usuarios = context.GetCollection<Usuario>("usuarios");
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync() =>
        await _usuarios.Find(_ => true).ToListAsync();

    public async Task<Usuario?> GetByIdAsync(string id) =>
        await _usuarios.Find(u => u.Id == id).FirstOrDefaultAsync();

    public async Task AddAsync(Usuario entity) =>
        await _usuarios.InsertOneAsync(entity);

    public async Task UpdateAsync(Usuario entity)
    {
        var filter = Builders<Usuario>.Filter.Eq(u => u.Id, entity.Id);
        await _usuarios.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveAsync(Usuario entity)
    {
        var filter = Builders<Usuario>.Filter.Eq(u => u.Id, entity.Id);
        await _usuarios.DeleteOneAsync(filter);
    }
}
