namespace PulseRegistrationSystem.Domain.Entities;

public class Endereco
{
    public string Rua { get; }
    public string Complemento { get; }
    public string Bairro { get; }
    public string Cep { get; }
    public string Cidade { get; }
    public string Estado { get; }

    public Endereco(string rua, string complemento, string bairro, string cep, string cidade, string estado)
    {
        Rua = ValidarObrigatorio(rua, nameof(Rua));
        Bairro = ValidarObrigatorio(bairro, nameof(Bairro));
        Cidade = ValidarObrigatorio(cidade, nameof(Cidade));
        Estado = ValidarObrigatorio(estado, nameof(Estado));
        Cep = ValidarObrigatorio(cep, nameof(cep));
        Complemento = complemento;
    }

    private static string ValidarObrigatorio(string valor, string nomeCampo)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException($"{nomeCampo} inválido");
        return valor;
    }

    
}