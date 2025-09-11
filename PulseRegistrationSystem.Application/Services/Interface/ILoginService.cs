using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;

namespace PulseRegistrationSystem.Application.Services.Interface;

public interface ILoginService
{
 
    Task<LoginResponseDto> BuscarPorCpfAsync(string cpf);
    Task<LoginResponseDto> BuscarPorIdAsync(Guid id);
    Task AtualizarSenhaAsync(Guid id, string novaSenha);
    Task DeleteAsync(Guid id);
    Task<LoginResponseDto> AutenticarAsync(LoginRequestDto loginRequestDto);
    Task<IEnumerable<LoginResponseDto>> ListarTodosAsync();
    Task DesbloquearUsuarioAsync(Guid id);
}