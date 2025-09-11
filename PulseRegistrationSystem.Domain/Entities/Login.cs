using PulseRegistrationSystem.Domain.SecurityConfiguration;

namespace PulseRegistrationSystem.Domain.Entities;

public class Login
{
    public Guid Id { get; private set; }

    public string NumeroCpf { get; private set; }

    public string SenhaHash { get; private set; }

    public Guid UsuarioId { get; private set; }

    public Usuario Usuario { get; private set; }

    public int TentativasLogin { get; private set; }
 
    public Login(string cpf, string senha, ISenhaHasher hasher)

    {

        Id = Guid.NewGuid();

        NumeroCpf = cpf;

        SenhaHash = hasher.GerarHash(senha);

        TentativasLogin = 0;

    }
 
    public bool EstaBloqueado => TentativasLogin >= 5;
 
    public void IncrementarTentativa() => TentativasLogin++;

    public void ResetarTentativas() => TentativasLogin = 0;

    public void Desbloquear() => TentativasLogin = 0;
 
    public void DefinirSenha(string senhaHash)

    {

        if (string.IsNullOrWhiteSpace(senhaHash))

            throw new ArgumentException("Hash de senha inválida.", nameof(senhaHash));
 
        SenhaHash = senhaHash;

    }

}