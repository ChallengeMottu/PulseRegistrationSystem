using Microsoft.EntityFrameworkCore;
using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Infraestructure.Persistence.Mappings;

namespace PulseRegistrationSystem.Infraestructure.Persistence;

public class PulseRegistrationSystemContext : DbContext

{

    public PulseRegistrationSystemContext(DbContextOptions<PulseRegistrationSystemContext> options)

        : base(options)

    {

    }
 
    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Login> Logins { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)

    {

        

        modelBuilder.ApplyConfiguration(new UsuarioMapping());
        modelBuilder.ApplyConfiguration(new LoginMapping());
        

        base.OnModelCreating(modelBuilder);

    }

}


