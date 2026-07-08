using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Usuario;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

[ApiController]
[Route("users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsersController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    /// <summary>Retorna os dados do usuário autenticado</summary>
    [HttpGet("me")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Me()
    {
        var id = User.ObterUsuarioId();
        var usuario = await _usuarioService.BuscarPorIdAsync(id);

        if (usuario is null)
            return NotFound(new { erro = "Usuário não encontrado" });

        return Ok(usuario);
    }

    /// <summary>Atualiza a senha do usuário</summary>
    [HttpPut("{id}/senha")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AtualizarSenha(int id, [FromBody] AtualizarSenhaRequestDto dto)
    {
        if (User.ObterUsuarioId() != id)
            return Forbid();

        await _usuarioService.AtualizarSenhaAsync(id, dto);
        return NoContent();
    }
}