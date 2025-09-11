using Microsoft.EntityFrameworkCore;
using PulseRegistrationSystem.Domain.Entities;

namespace PulseRegistrationSystem.Infraestructure.Persistence;

public class PulseRegistrationSystemContext(DbContextOptions<PulseRegistrationSystemContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Login> Logins { get; set; }
}