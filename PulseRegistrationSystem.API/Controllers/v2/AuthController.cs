using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PulseRegistrationSystem.Application.DTOs.Request;
using PulseRegistrationSystem.Application.DTOs.Response;
using PulseRegistrationSystem.Application.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PulseRegistrationSystem.API.Controllers.v2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;
        private readonly IUsuarioService _usuarioService;

        public AuthController(
            IConfiguration configuration,
            ILoginService loginService,
            IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _loginService = loginService;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="dto">Objeto contendo os dados do usuário a ser registrado.</param>
        /// <returns>O usuário criado.</returns>
        /// <response code="201">Usuário registrado com sucesso.</response>
        /// <response code="400">Dados inválidos fornecidos para registro.</response>
        [HttpPost("registrar")]
        [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Registrar([FromBody] UsuarioRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    mensagem = "Dados de entrada inválidos.",
                    erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });

            var usuario = await _usuarioService.CriarAsync(dto);
            return CreatedAtAction(nameof(Registrar), new { id = usuario.Id }, usuario);
        }

        /// <summary>
        /// Realiza login e retorna um token JWT para autenticação.
        /// </summary>
        /// <param name="loginDto">Objeto contendo login e senha do usuário.</param>
        /// <returns>Token JWT se autenticado com sucesso.</returns>
        /// <response code="200">Autenticação bem-sucedida, token JWT retornado.</response>
        /// <response code="401">Credenciais inválidas.</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(object), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            try
            {
                var usuario = await _loginService.AutenticarAsync(loginDto);

                if (usuario == null)
                    return Unauthorized(new { mensagem = "Credenciais inválidas." });

                var token = GerarJwt(usuario);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    mensagem = "Falha na autenticação: credenciais inválidas.",
                    detalhes = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    mensagem = "Erro ao autenticar usuário.",
                    detalhes = ex.Message
                });
            }
        }

        /// <summary>
        /// Gera um token JWT para o usuário autenticado.
        /// </summary>
        private string GerarJwt(LoginResponseDto usuario)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"]!;
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expirationMinutes = double.Parse(jwtSettings["ExpirationMinutes"]!);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
                new Claim("cpf", usuario.NumeroCpf),
                new Claim("nome", usuario.NomeUsuario),
                new Claim("usuarioId", usuario.UsuarioId ?? string.Empty),
                new Claim(ClaimTypes.Role, "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
