using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Application.Services.Interface;


namespace PulseRegistrationSystem.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")] // 🔹 Adicione esta linha
    [Route("api/v{version:apiVersion}/[controller]")]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
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
                var response = await loginService.AutenticarAsync(loginRequestDto);
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

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LoginResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LoginResponseDto>>> ListarTodos()
        {
            try
            {
                var lista = await loginService.ListarTodosAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro ao listar logins.", detalhes = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseDto>> BuscarPorId(string id)
        {
            try
            {
                var login = await loginService.BuscarPorIdAsync(id);
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

        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponseDto>> BuscarPorCpf(string cpf)
        {
            try
            {
                var login = await loginService.BuscarPorCpfAsync(cpf);
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
                await loginService.AtualizarSenhaAsync(id, novaSenha);
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

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deletar(string id)
        {
            try
            {
                await loginService.DeleteAsync(id);
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

        [HttpPost("{id}/desbloquear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Desbloquear(string id)
        {
            try
            {
                await loginService.DesbloquearUsuarioAsync(id);
                return Ok(new { mensagem = "Usuário desbloqueado com sucesso." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensagem = "Erro ao desbloquear usuário.", detalhes = ex.Message });
            }
        }
    }
}
