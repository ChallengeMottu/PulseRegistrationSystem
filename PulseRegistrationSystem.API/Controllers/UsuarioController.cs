using Microsoft.AspNetCore.Mvc;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Application.Services.Interface;
using PulseRegistrationSystem.Domain.Exceptions;

namespace PulseRegistrationSystem.API.Controllers;

[Route("api/usuario")]
[ApiController]
public class UsuarioController(IUsuarioService usuarioService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<UsuarioResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> ListarTodos()
    {
        
            var usuarios = await usuarioService.ListarTodosAsync();
            return Ok(usuarios);
        
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UsuarioResponseDto>> BuscarPorId(string id)
    {
        try
        {
            var usuarioDto = await usuarioService.BuscarPorIdAsync(id);
            return Ok(usuarioDto);
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }

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
            var usuarioCriado = await usuarioService.CriarAsync(usuarioRequest);
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
            await usuarioService.AtualizarAsync(id, usuarioRequest);
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

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deletar(string id)
    {
        try
        {
            await usuarioService.DeletarAsync(id);
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
