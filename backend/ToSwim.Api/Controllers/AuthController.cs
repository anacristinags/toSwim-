using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Auth;
using ToSwim.Application.Interfaces;

namespace ToSwim.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>Cadastra um novo usuário</summary>
    [HttpPost("registro")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registro([FromBody] RegistroRequestDto dto)
    {
        var resultado = await _authService.RegistrarAsync(dto);
        return CreatedAtAction(nameof(Registro), resultado);
    }

    /// <summary>Autentica o usuário e retorna o token</summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var resultado = await _authService.LoginAsync(dto);
        return Ok(resultado);
    }
}