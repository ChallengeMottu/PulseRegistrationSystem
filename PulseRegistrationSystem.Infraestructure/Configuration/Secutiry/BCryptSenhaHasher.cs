using PulseRegistrationSystem.Domain.SecurityConfiguration;

namespace PulseRegistrationSystem.Infraestructure.Configuration.Secutiry;

public class BCryptSenhaHasher : ISenhaHasher
{
    public string GerarHash(string senha) => BCrypt.Net.BCrypt.HashPassword(senha);
    public bool VerificarHash(string senha, string hash) => BCrypt.Net.BCrypt.Verify(senha, hash);
}