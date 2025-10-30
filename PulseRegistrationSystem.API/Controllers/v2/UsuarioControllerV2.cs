using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Application.Services.Interface;
using PulseRegistrationSystem.Domain.Exceptions;

namespace PulseRegistrationSystem.API.Controllers.v2
{
    /// <summary>
    /// Controller versão 2.0 responsável pelo gerenciamento de usuários.
    /// Todos os endpoints desta versão requerem autenticação via JWT.
    /// </summary>
    [ApiController]
    [Authorize]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuarioControllerV2 : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Construtor do UsuarioControllerV2.
        /// </summary>
        /// <param name="usuarioService">Serviço responsável pelas operações de usuário.</param>
        public UsuarioControllerV2(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> ListarTodos()
        {
            var usuarios = await _usuarioService.ListarTodosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Busca um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser buscado.</param>
        /// <returns>Dados do usuário encontrado.</returns>
        /// <response code="200">Usuário encontrado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UsuarioResponseDto>> BuscarPorId(string id)
        {
            var usuario = await _usuarioService.BuscarPorIdAsync(id);
            if (usuario == null)
                return NotFound(new { mensagem = "Usuário não encontrado." });
            return Ok(usuario);
        }

        /// <summary>
        /// Deleta um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser deletado.</param>
        /// <response code="200">Cadastro deletado com sucesso.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro interno ao deletar usuário.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deletar(string id)
        {
            try
            {
                await _usuarioService.DeletarAsync(id);
                return Ok(new { mensagem = "Cadastro deletado com sucesso." });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao deletar usuário.", detalhes = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de um usuário.
        /// </summary>
        /// <param name="id">ID do usuário a ser atualizado.</param>
        /// <param name="usuarioRequest">Objeto contendo os novos dados do usuário.</param>
        /// <response code="200">Cadastro atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos ou violação de regras de validação.</response>
        /// <response code="404">Usuário não encontrado.</response>
        /// <response code="500">Erro interno ao atualizar usuário.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Atualizar(string id, [FromBody] UsuarioRequestDto usuarioRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    mensagem = "Dados de entrada inválidos.",
                    erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });

            try
            {
                await _usuarioService.AtualizarAsync(id, usuarioRequest);
                return Ok(new { mensagem = "Cadastro atualizado com sucesso." });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex) when (ex is InvalidUserDataException || ex is InvalidUserAgeException || ex is ArgumentException)
            {
                var msg = ex switch
                {
                    InvalidUserAgeException => "Erro de validação: usuário deve ser maior de 18 anos.",
                    InvalidUserDataException => "Erro de validação: " + ex.Message,
                    ArgumentException => "Erro de validação do endereço: " + ex.Message,
                    _ => ex.Message
                };
                return BadRequest(new { mensagem = msg });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao atualizar usuário.", detalhes = ex.Message });
            }
        }
    }
}
