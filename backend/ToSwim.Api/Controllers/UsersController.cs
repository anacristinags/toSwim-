using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Usuario;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de dados cadastrais e segurança dos atletas.
/// </summary>
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

    /// <summary>
    /// Retorna os dados de perfil cadastrais do usuário autenticado no sistema.
    /// </summary>
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

    /// <summary>
    /// Atualiza de forma segura a senha de login do nadador.
    /// </summary>
    /// <remarks>
    /// A senha é encriptada no backend usando hashes BCrypt antes de ser persistida no banco de dados PostgreSQL.
    /// </remarks>
    /// <param name="id">Código identificador do usuário.</param>
    /// <param name="dto">Modelo contendo a senha atual e a nova senha desejada.</param>
    [HttpPut("{id}/senha")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AtualizarSenha(int id, [FromBody] AtualizarSenhaRequestDto dto)
    {
        if (User.ObterUsuarioId() != id)
            return Forbid();

        await _usuarioService.AtualizarSenhaAsync(id, dto); // Ajuste o nome se não possuir caracteres extras
        return NoContent();
    }
}