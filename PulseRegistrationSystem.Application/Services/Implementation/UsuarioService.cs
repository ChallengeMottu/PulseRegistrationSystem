using AutoMapper;
using MongoDB.Bson;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Application.Services.Interface;
using PulseRegistrationSystem.Domain.Entities;
using PulseRegistrationSystem.Domain.Exceptions;
using PulseRegistrationSystem.Domain.SecurityConfiguration;
using PulseRegistrationSystem.Infraestructure.Repositories.Interface;

namespace PulseRegistrationSystem.Application.Services.Implementation;

public class UsuarioService : IUsuarioService
{
    private readonly IMethodsRepository<Usuario> _usuarioRepository;

    private readonly IMapper _mapper;

    private readonly ISenhaHasher _senhaHasher;
    private readonly ILoginRepository _loginRepository; 
 
    public UsuarioService(

        IMethodsRepository<Usuario> usuarioRepository,

        IMapper mapper,

        ISenhaHasher senhaHasher, ILoginRepository loginRepository)

    {

        _usuarioRepository = usuarioRepository;

        _mapper = mapper;

        _senhaHasher = senhaHasher;
        _loginRepository = loginRepository;

    }
 
    public async Task<UsuarioResponseDto> CriarAsync(UsuarioRequestDto usuarioRequestDto)
    {
        var usuario = _mapper.Map<Usuario>(usuarioRequestDto);
        usuario.Id = ObjectId.GenerateNewId().ToString();

        var login = new Login(usuarioRequestDto.Cpf, usuarioRequestDto.Senha, _senhaHasher, usuario.Id);
        usuario.Login = login;

        
        await _usuarioRepository.AddAsync(usuario);
        await _loginRepository.AddAsync(login); 

        return _mapper.Map<UsuarioResponseDto>(usuario);
    }
 
    public async Task<IEnumerable<UsuarioResponseDto>> ListarTodosAsync()

    {

        var usuarios = await _usuarioRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<UsuarioResponseDto>>(usuarios);

    }
 
    public async Task<UsuarioResponseDto> BuscarPorIdAsync(string id)

    {

        var usuario = await _usuarioRepository.GetByIdAsync(id);

        if (usuario == null)

            throw new UserNotFoundException(id);
 
        return _mapper.Map<UsuarioResponseDto>(usuario);

    }
 
    public async Task AtualizarAsync(string id, UsuarioRequestDto dto)

    {

        var usuario = await _usuarioRepository.GetByIdAsync(id);

        if (usuario == null)

            throw new UserNotFoundException(id);

        _mapper.Map(dto, usuario);

        await _usuarioRepository.UpdateAsync(usuario);

    }
 
    public async Task DeletarAsync(string id)

    {

        var usuario = await _usuarioRepository.GetByIdAsync(id);

        if (usuario == null)

            throw new UserNotFoundException(id);
 
        await _usuarioRepository.RemoveAsync(usuario);

    }

}


