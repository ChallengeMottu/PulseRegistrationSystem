using System.Text.RegularExpressions;
using PulseRegistrationSystem.Domain.Enums;
using PulseRegistrationSystem.Domain.Exceptions;

namespace PulseRegistrationSystem.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; }

    public string Cpf { get; private set; }

    public DateTime DataNascimento { get; private set; }
 
    public Endereco FilialMottu { get; private set; }
 
    public string Email { get; private set; }
 
    public FuncaoEnum Funcao { get; private set; }

    public DateTime DataCadastro { get; private set; } = DateTime.UtcNow;
 
    public Login Login { get; set; }
 
    protected Usuario(){}
 
    public Usuario(string nome, string cpf, DateTime dataNascimento,
        Endereco filialMottu, string email, FuncaoEnum funcao, Login login)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Cpf = cpf;
        DataNascimento = dataNascimento;
        FilialMottu = filialMottu;
        Email = email;
        Funcao = funcao;
        Login = login;

        Validar(); 
    }

    public void Validar()

    {

        ValidarStringObrigatoria(Nome, nameof(Nome));

        ValidarStringObrigatoria(Cpf, nameof(Cpf), @"^\d{11}$");

        ValidarStringObrigatoria(Email, nameof(Email), @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        ValidarMaioridade();

    }
 
    private void ValidarStringObrigatoria(string valor, string nomeCampo, string? regex = null)

    {

        if (string.IsNullOrWhiteSpace(valor))

            throw new InvalidUserDataException($"{nomeCampo} não pode ser vazio.");
 
        if (regex != null && !Regex.IsMatch(valor, regex))

            throw new InvalidUserDataException($"{nomeCampo} está em formato inválido.");

    }
 
    private void ValidarMaioridade()

    {

        var hoje = DateTime.Today;

        var idade = hoje.Year - DataNascimento.Year;

        if (DataNascimento > hoje.AddYears(-idade)) idade--;

        if (idade < 18)

            throw new InvalidUserAgeException();

    }

}