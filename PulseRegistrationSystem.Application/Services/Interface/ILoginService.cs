using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;

namespace PulseRegistrationSystem.Application.Services.Interface;

public interface ILoginService
{
 
    Task<LoginResponseDto> BuscarPorCpfAsync(string cpf);
    Task<LoginResponseDto> BuscarPorIdAsync(string id);
    Task AtualizarSenhaAsync(string id, string novaSenha);
    Task DeleteAsync(string id);
    Task<LoginResponseDto> AutenticarAsync(LoginRequestDto loginRequestDto);
    Task<IEnumerable<LoginResponseDto>> ListarTodosAsync();
    Task DesbloquearUsuarioAsync(string id);
}