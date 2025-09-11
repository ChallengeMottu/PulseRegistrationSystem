using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;

namespace PulseRegistrationSystem.Application.Services.Interface;

public interface IUsuarioService 
{
    Task<UsuarioResponseDto> CriarAsync(UsuarioRequestDto usuarioRequestDto);
    Task<IEnumerable<UsuarioResponseDto>> ListarTodosAsync();
    Task<UsuarioResponseDto> BuscarPorIdAsync(Guid id);
    Task AtualizarAsync(Guid id, UsuarioRequestDto dto);
    Task DeletarAsync(Guid id);
    
}
