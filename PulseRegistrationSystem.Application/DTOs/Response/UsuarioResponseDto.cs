using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Domain.Enums;

namespace PulseRegistrationSystem.Application.DTOs.Response;

public class UsuarioResponseDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
    public Endereco FilialMottu { get; set; }
    public string Email { get; set; }
    public FuncaoEnum Funcao { get; set; }
 
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
}