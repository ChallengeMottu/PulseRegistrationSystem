using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PulseRegistrationSystem.Domain.Entities;

namespace PulseRegistrationSystem.Infraestructure.Persistence.Mappings;

public class LoginMapping : IEntityTypeConfiguration<Login>

{

    public void Configure(EntityTypeBuilder<Login> builder)

    {

        builder.ToTable("Login");
 
        builder.HasKey(x => x.Id);
 
        builder

            .Property(x => x.NumeroCpf)

            .HasMaxLength(11)

            .IsRequired();
 
        builder

            .Property(x => x.SenhaHash)

            .IsRequired();
 
        builder

            .Property(x => x.UsuarioId)

            .IsRequired();
 
        builder

            .Property(x => x.TentativasLogin)

            .IsRequired();
 
 
 
        builder

            .HasOne(l => l.Usuario)

            .WithOne(c => c.Login)

            .HasForeignKey<Login>(l => l.UsuarioId)

            .OnDelete(DeleteBehavior.Cascade);

    }

}
