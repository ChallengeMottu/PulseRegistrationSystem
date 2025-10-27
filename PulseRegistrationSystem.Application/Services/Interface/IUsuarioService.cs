using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;

namespace PulseRegistrationSystem.Application.Services.Interface;

public interface IUsuarioService 
{
    Task<UsuarioResponseDto> CriarAsync(UsuarioRequestDto usuarioRequestDto);
    Task<IEnumerable<UsuarioResponseDto>> ListarTodosAsync();
    Task<UsuarioResponseDto> BuscarPorIdAsync(string id);
    Task AtualizarAsync(string id, UsuarioRequestDto dto);
    Task DeletarAsync(string id);
    
}
