using Microsoft.EntityFrameworkCore;
using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Infraestructure.Persistence;
using PulseRegistrationSystem.Infraestructure.Repositories.Interface;

namespace PulseRegistrationSystem.Infraestructure.Repositories.Implementation;

public class UsuarioRepository : IMethodsRepository<Usuario>

{

    private readonly PulseRegistrationSystemContext _context;

    private readonly DbSet<Usuario> _dbSet;
 
    public UsuarioRepository(PulseRegistrationSystemContext context)

    {

        _context = context;

        _dbSet = context.Set<Usuario>();

    }

    public async Task AddAsync(Usuario usuario)

    {

        await _dbSet.AddAsync(usuario);

        await _context.SaveChangesAsync();

    }
 
    public async Task<IEnumerable<Usuario>> GetAllAsync()

    {

        return await _dbSet.ToListAsync();

    }
 
    public async Task<Usuario?> GetByIdAsync(Guid id)

    {

        return await _dbSet.FindAsync(id);

    }
 
    public async Task RemoveAsync(Usuario usuario)

    {

        _dbSet.Remove(usuario);

        await _context.SaveChangesAsync();

    }
 
    public async Task UpdateAsync(Usuario usuario)

    {

        _dbSet.Update(usuario);

        await _context.SaveChangesAsync();

    }
 
}
