namespace PulseRegistrationSystem.Application.DTOs.Response;

public class LoginResponseDto
{
    public string Id { get; set; }
    public string NumeroCpf { get; set; }
    public int TentativasLogin { get; set; }
    public string NomeUsuario { get; set; }
 
    public string UsuarioId { get; set; }
}