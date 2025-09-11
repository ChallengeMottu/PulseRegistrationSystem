using Microsoft.EntityFrameworkCore;
using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Infraestructure.Persistence.Mappings;

namespace PulseRegistrationSystem.Infraestructure.Persistence;

public class PulseRegistrationSystemContext(DbContextOptions<PulseRegistrationSystemContext> options) : DbContext(options)

{
 
    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Login> Logins { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {

        

        modelBuilder.ApplyConfiguration(new UsuarioMapping());
        modelBuilder.ApplyConfiguration(new LoginMapping());
        

        

    }

}


