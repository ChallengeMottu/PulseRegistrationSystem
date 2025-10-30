using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Application.Services.Interface;

namespace PulseRegistrationSystem.API.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar autenticação e login de usuários.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        /// <summary>
        /// Construtor do LoginController.
        /// </summary>
        /// <param name="loginService">Serviço responsável pelas operações de login.</param>
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        /// <summary>
        /// Autentica um usuário e retorna os dados de login, incluindo token JWT.
        /// </summary>
        /// <param name="loginRequestDto">Objeto contendo CPF/email e senha do usuário.</param>
        /// <returns>Dados do login autenticado.</returns>
        /// <response code="200">Usuário autenticado com sucesso.</response>
        /// <response code="401">Credenciais inválidas.</response>
        /// <response code="403">Usuário não autorizado.</response>
        /// <response code="500">Erro interno ao autenticar usuário.</response>
        [HttpPost("autenticar")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(object), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Autenticar([FromBody] LoginRequestDto loginRequestDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    mensagem = "Dados de entrada inválidos.",
                    erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            try
            {
                var response = await _loginService.AutenticarAsync(loginRequestDto);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new { mensagem = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro ao autenticar usuário.", detalhes = ex.Message });
            }
        }

        /// <summary>
        /// Lista todos os logins cadastrados.
        /// </summary>
        /// <returns>Lista de usuários cadastrados.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        /// <response code="500">Erro interno ao listar logins.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LoginResponseDto>>> ListarTodos()
        {
            try
            {
                var lista = await _loginService.ListarTodosAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro ao listar logins.", detalhes = ex.Message });
            }
        }

        /// <summary>
        /// Busca um login pelo ID do usuário.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Dados do login encontrado.</returns>
        /// <response code="200">Login encontrado.</response>
        /// <response code="404">Login não encontrado.</response>
        /// <response code="500">Erro interno ao buscar login.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseDto>> BuscarPorId(string id)
        {
            try
            {
                var login = await _loginService.BuscarPorIdAsync(id);
                return Ok(login);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro ao buscar login.", detalhes = ex.Message });
            }
        }

        /// <summary>
        /// Busca um login pelo CPF do usuário.
        /// </summary>
        /// <param name="cpf">CPF do usuário.</param>
        /// <returns>Dados do login encontrado.</returns>
        /// <response code="200">Login encontrado.</response>
        /// <response code="404">Login não encontrado.</response>
        /// <response code="500">Erro interno ao buscar login.</response>
        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseDto>> BuscarPorCpf(string cpf)
        {
            try
            {
                var login = await _loginService.BuscarPorCpfAsync(cpf);
                return Ok(login);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro ao buscar login.", detalhes = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza a senha do usuário.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <param name="novaSenha">Nova senha a ser cadastrada.</param>
        /// <response code="200">Senha atualizada com sucesso.</response>
        /// <response code="400">Senha inválida.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro interno ao atualizar senha.</response>
        [HttpPut("{id}/senha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarSenha(string id, [FromBody] string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(novaSenha))
                return BadRequest(new { mensagem = "Nova senha inválida." });

            try
            {
                await _loginService.AtualizarSenhaAsync(id, novaSenha);
                return Ok(new { mensagem = "Senha atualizada com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro ao atualizar senha.", detalhes = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um login pelo ID do usuário.
        /// </summary>
        /// <param name="id">ID do usuário a ser deletado.</param>
        /// <response code="200">Login deletado com sucesso.</response>
        /// <response code="404">Login não encontrado.</response>
        /// <response code="500">Erro interno ao deletar login.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deletar(string id)
        {
            try
            {
                await _loginService.DeleteAsync(id);
                return Ok(new { mensagem = "Login deletado com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro ao deletar login.", detalhes = ex.Message });
            }
        }
    }
}
