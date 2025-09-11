using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Domain.Enums;

namespace PulseRegistrationSystem.Application.DTOs.Request;

public class UsuarioRequestDto

{

    public string Nome { get; set; }
 
    

    public string Cpf { get; set; }

    public DateTime DataNascimento { get; set; }
 
    

    public EnderecoRequestDto FilialMottu { get; set; }
 
    

    public string Email { get; set; }
 
    

    public FuncaoEnum Funcao { get; set; }
 
    

    public string Senha { get; set; }

}

