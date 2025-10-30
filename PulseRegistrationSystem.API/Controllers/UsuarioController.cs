using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Application.Services.Interface;
using PulseRegistrationSystem.Domain.Exceptions;

namespace PulseRegistrationSystem.API.Controllers
{
    /// <summary>
    /// Controller responsável pelo gerenciamento de usuários (CRUD).
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")] 
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsuarioController: ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        /// <summary>
        /// Construtor do controller de usuários.
        /// </summary>
        /// <param name="usuarioService">Serviço de usuário para operações CRUD.</param>
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Lista todos os usuários cadastrados.
        /// </summary>
        /// <remarks>
        /// Retorna todos os usuários disponíveis no sistema.
        /// </remarks>
        /// <returns>Lista de objetos <see cref="UsuarioResponseDto"/> representando os usuários.</returns>
        /// <response code="200">Retorna a lista de usuários.</response>
        /// <response code="500">Ocorre um erro interno ao listar os usuários.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UsuarioResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> ListarTodos()
        {
            var usuarios = await _usuarioService.ListarTodosAsync();
            return Ok(usuarios);
        }

        /// <summary>
        /// Busca um usuário pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser buscado.</param>
        /// <returns>Objeto <see cref="UsuarioResponseDto"/> correspondente ao usuário encontrado.</returns>
        /// <response code="200">Usuário encontrado com sucesso.</response>
        /// <response code="404">Usuário não encontrado para o ID fornecido.</response>
        /// <response code="500">Ocorre um erro interno ao buscar o usuário.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioResponseDto>> BuscarPorId(string id)
        {
            try
            {
                var usuarioDto = await _usuarioService.BuscarPorIdAsync(id);
                return Ok(usuarioDto);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="usuarioRequest">Objeto <see cref="UsuarioRequestDto"/> com os dados do usuário.</param>
        /// <returns>Objeto <see cref="UsuarioResponseDto"/> criado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Dados de entrada inválidos ou falha de validação.</response>
        /// <response code="500">Erro interno ao criar o usuário.</response>
        [HttpPost]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioResponseDto>> Criar([FromBody] UsuarioRequestDto usuarioRequest)
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
                var usuarioCriado = await _usuarioService.CriarAsync(usuarioRequest);
                return CreatedAtAction(nameof(BuscarPorId), new { id = usuarioCriado.Id }, usuarioCriado);
            }
            catch (InvalidUserDataException ex)
            {
                return BadRequest(new { mensagem = "Erro de validação: " + ex.Message });
            }
            catch (InvalidUserAgeException)
            {
                return BadRequest(new { mensagem = "Erro de validação: usuário deve ser maior de 18 anos." });
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(new { mensagem = "Erro de validação do endereço: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao criar usuário.", detalhes = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente.
        /// </summary>
        /// <param name="id">ID do usuário a ser atualizado.</param>
        /// <param name="usuarioRequest">Objeto <see cref="UsuarioRequestDto"/> com os novos dados do usuário.</param>
        /// <returns>Mensagem de sucesso ou objeto de erro.</returns>
        /// <response code="200">Usuário atualizado com sucesso.</response>
        /// <response code="400">Falha de validação dos dados fornecidos.</response>
        /// <response code="404">Usuário não encontrado para o ID fornecido.</response>
        /// <response code="500">Erro interno ao atualizar usuário.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Atualizar(string id, [FromBody] UsuarioRequestDto usuarioRequest)
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
                await _usuarioService.AtualizarAsync(id, usuarioRequest);
                return Ok(new { mensagem = "Cadastro atualizado com sucesso." });
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidUserDataException ex)
            {
                return BadRequest(new { mensagem = "Erro de validação: " + ex.Message });
            }
            catch (InvalidUserAgeException)
            {
                return BadRequest(new { mensagem = "Erro de validação: usuário deve ser maior de 18 anos." });
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(new { mensagem = "Erro de validação do endereço: " + ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro ao atualizar usuário.", detalhes = ex.Message });
            }
        }

        /// <summary>
        /// Deleta um usuário existente pelo seu ID.
        /// </summary>
        /// <param name="id">ID do usuário a ser deletado.</param>
        /// <returns>Mensagem de sucesso ou objeto de erro.</returns>
        /// <response code="200">Usuário deletado com sucesso.</response>
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
    }
}
