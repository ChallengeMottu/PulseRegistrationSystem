using Microsoft.AspNetCore.Mvc;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Application.Services.Interface;

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
    public async Task<ActionResult<UsuarioResponseDto>> BuscarPorId(Guid id)
    {
        try
        {
            var usuarioDto = await usuarioService.BuscarPorIdAsync(id);
            return Ok(usuarioDto);
        }
        catch (KeyNotFoundException ex)
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
            return BadRequest(new
            {
                mensagem = "Dados de entrada inválidos.",
                erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
            });
 
        var usuarioCriado = await usuarioService.CriarAsync(usuarioRequest);
        return CreatedAtAction(nameof(BuscarPorId), new { id = usuarioCriado.Id }, usuarioCriado);
    }

    
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] UsuarioRequestDto usuarioRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { mensagem = "Dados de entrada inválidos.", erros = ModelState });
 
        try
        {
            await usuarioService.AtualizarAsync(id, usuarioRequest);
            return Ok(new { mensagem = "Cadastro atualizado com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(object), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Deletar(Guid id)
    {
        try
        {
            await usuarioService.DeletarAsync(id);
            return Ok(new { mensagem = "Cadastro deletado com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { mensagem = ex.Message });
        }
    }
}