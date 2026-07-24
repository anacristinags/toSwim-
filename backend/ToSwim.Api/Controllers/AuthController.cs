using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Auth;
using ToSwim.Application.Interfaces;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável pelos endpoints públicos de registro e autenticação de atletas (JWT).
/// </summary>
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Cadastra um novo atleta no sistema e retorna seus dados de perfil e o token JWT inicial.
    /// </summary>
    /// <param name="dto">Dados de cadastro contendo nome, e-mail único e senha.</param>
    [HttpPost("registro")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registro([FromBody] RegistroRequestDto dto)
    {
        var resultado = await _authService.RegistrarAsync(dto);
        return CreatedAtAction(nameof(Registro), resultado);
    }

    /// <summary>
    /// Autentica o atleta por e-mail e senha, retornando o token JWT para autorização de rotas protegidas.
    /// </summary>
    /// <param name="dto">Credenciais de acesso (e-mail e senha).</param>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var resultado = await _authService.LoginAsync(dto);
        return Ok(resultado);
    }
}