namespace PulseRegistrationSystem.Domain.SecurityConfiguration;

public interface ISenhaHasher
{
    string GerarHash(string senha);
    bool VerificarHash(string senha, string hash);
}