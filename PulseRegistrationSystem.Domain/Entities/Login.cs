using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PulseRegistrationSystem.Domain.SecurityConfiguration;

namespace PulseRegistrationSystem.Domain.Entities;

public class Login
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; private set; } = null!; 

    public string NumeroCpf { get; private set; } = null!;
    public string SenhaHash { get; private set; } = null!;
    public string UsuarioId { get; private set; } = null!;
    public int TentativasLogin { get; private set; }

    public Login() {}

    public Login(string cpf, string senha, ISenhaHasher hasher, string usuarioId)
    {
        Id = ObjectId.GenerateNewId().ToString();
        NumeroCpf = cpf;
        SenhaHash = hasher.GerarHash(senha);
        UsuarioId = usuarioId; // Link automático
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