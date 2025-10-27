using AutoMapper;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Application.Services.Interface;
using PulseRegistrationSystem.Domain.SecurityConfiguration;
using PulseRegistrationSystem.Infraestructure.Repositories.Interface;

namespace PulseRegistrationSystem.Application.Services.Implementation;

public class LoginService : ILoginService
{
    private readonly ILoginRepository _loginRepository;

    private readonly IMapper _mapper;

    private readonly ISenhaHasher _senhaHasher;
 
public LoginService(ILoginRepository loginRepository, IMapper mapper, ISenhaHasher senhaHasher)

    {

        _loginRepository = loginRepository;

        _mapper = mapper;

        _senhaHasher = senhaHasher;

    }
 
    public async Task<LoginResponseDto> BuscarPorCpfAsync(string cpf)

    {

        var login = await _loginRepository.GetByCpfAsync(cpf);

        if (login == null)

            throw new KeyNotFoundException("Login não encontrado.");
 
        return _mapper.Map<LoginResponseDto>(login);

    }
 
    public async Task<LoginResponseDto> BuscarPorIdAsync(string id)

    {

        var login = await _loginRepository.GetByIdAsync(id);

        if (login == null)

            throw new KeyNotFoundException("Login não encontrado.");
 
        return _mapper.Map<LoginResponseDto>(login);

    }
 
    public async Task AtualizarSenhaAsync(string id, string novaSenha)

    {

        var login = await _loginRepository.GetByIdAsync(id);

        if (login == null)

            throw new KeyNotFoundException("Login não encontrado.");
 
        var hash = _senhaHasher.GerarHash(novaSenha);

        login.DefinirSenha(hash);
 
        await _loginRepository.UpdateAsync(login);

    }
 
    public async Task DeleteAsync(string id)

    {

        var login = await _loginRepository.GetByIdAsync(id);

        if (login == null)

            throw new KeyNotFoundException("Login não encontrado.");
 
        await _loginRepository.RemoveAsync(login);

    }
 
    public async Task<LoginResponseDto> AutenticarAsync(LoginRequestDto loginRequestDto)

    {

        var login = await _loginRepository.GetByCpfAsync(loginRequestDto.NumeroCpf);
 
        if (login == null)

            throw new UnauthorizedAccessException("CPF ou senha inválidos.");
 
        if (login.EstaBloqueado)

            throw new InvalidOperationException("Usuário bloqueado por múltiplas tentativas de login falhas.");
 
        bool senhaCorreta = _senhaHasher.VerificarHash(loginRequestDto.Senha, login.SenhaHash);
 
        if (senhaCorreta)

            login.ResetarTentativas();

        else

            login.IncrementarTentativa();
 
        await _loginRepository.UpdateAsync(login);
 
        if (!senhaCorreta)

            throw new UnauthorizedAccessException("CPF ou senha inválidos.");
 
        return _mapper.Map<LoginResponseDto>(login);

    }
 
    public async Task<IEnumerable<LoginResponseDto>> ListarTodosAsync()

    {

        var logins = await _loginRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<LoginResponseDto>>(logins);

    }
 
    public async Task DesbloquearUsuarioAsync(string id)

    {

        var login = await _loginRepository.GetByIdAsync(id);

        if (login == null)

            throw new KeyNotFoundException("Login não encontrado.");
 
        login.Desbloquear();

        await _loginRepository.UpdateAsync(login);

    }

}

 
