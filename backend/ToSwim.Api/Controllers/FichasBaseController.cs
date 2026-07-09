using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.FichaBase;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

[ApiController]
[Route("fichas-base")]
[Authorize]
public class FichasBaseController : ControllerBase
{
    private readonly IFichaBaseService _fichaService;

    public FichasBaseController(IFichaBaseService fichaService)
    {
        _fichaService = fichaService;
    }

    /// <summary>Lista as fichas bases ativas do usuário</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FichaBaseResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar()
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.ListarPorUsuarioAsync(codUsuario);
        return Ok(resultado);
    }

    /// <summary>Retorna os detalhes de uma ficha base com suas séries</summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FichaBaseResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.BuscarPorIdAsync(id, codUsuario);
        return Ok(resultado);
    }

    /// <summary>Cria uma nova ficha base (Valida limite de 5 ativas)</summary>
    [HttpPost]
    [ProducesResponseType(typeof(FichaBaseResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] FichaBaseRequestDto dto)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.CriarAsync(codUsuario, dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = resultado.CodFicha }, resultado);
    }

    /// <summary>Atualiza o cabeçalho de uma ficha</summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(FichaBaseResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(int id, [FromBody] FichaBaseRequestDto dto)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.AtualizarAsync(id, codUsuario, dto);
        return Ok(resultado);
    }

    /// <summary>Altera o status da ficha (1 = Ativo, 0 = Inativo)</summary>
    [HttpPut("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AlterarStatus(int id, [FromBody] short novoStatus)
    {
        var codUsuario = User.ObterUsuarioId();
        await _fichaService.AlterarStatusAsync(id, codUsuario, novoStatus);
        return NoContent();
    }

    /// <summary>Deleta a ficha base (Inativa se houver histórico de treinos)</summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(int id)
    {
        var codUsuario = User.ObterUsuarioId();
        await _fichaService.DeletarAsync(id, codUsuario);
        return NoContent();
    }

    /// <summary>Duplica a ficha e todas as suas séries associadas</summary>
    [HttpPost("{id}/duplicar")]
    [ProducesResponseType(typeof(FichaBaseResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Duplicar(int id)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.DuplicarAsync(id, codUsuario);
        return CreatedAtAction(nameof(ObterPorId), new { id = resultado.CodFicha }, resultado);
    }
}