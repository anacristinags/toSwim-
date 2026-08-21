using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.FichaBase;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller respons?vel pela gest?o das s?ries de exerc?cios vinculadas ?s Fichas Base.
/// </summary>
[ApiController]
[Route("fichas-base")]
[Authorize]
public class SeriesFichaController : ControllerBase
{
    private readonly ISerieFichaService _serieService;

    public SeriesFichaController(ISerieFichaService serieService)
    {
        _serieService = serieService;
    }

    /// <summary>
    /// Lista todas as s?ries de exerc?cios que comp?em uma determinada ficha base.
    /// </summary>
    /// <param name="id">C?digo identificador da Ficha Base.</param>
    [HttpGet("{id}/series")]
    [ProducesResponseType(typeof(IEnumerable<SerieFichaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(int id)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _serieService.ListarSeriesPorFichaAsync(id, codUsuario);
        return Ok(resultado);
    }

    /// <summary>
    /// Adiciona uma nova s?rie de exerc?cio f?sica (nado, dist?ncia, repeti??es, pausa) na ficha de nata??o.
    /// </summary>
    /// <param name="id">C?digo identificador da Ficha Base.</param>
    /// <param name="dto">Informa??es f?sicas da s?rie planejada.</param>
    [HttpPost("{id}/series")]
    [ProducesResponseType(typeof(SerieFichaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AdicionarSerie(int id, [FromBody] SerieFichaRequestDto dto)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _serieService.AdicionarSerieAsync(id, codUsuario, dto);
        return CreatedAtAction(nameof(Listar), new { id = id }, resultado);
    }

    /// <summary>
    /// Duplica uma serie existente, inserindo a copia imediatamente apos a original e reordenando as demais.
    /// </summary>
    /// <param name="idSerie">Codigo identificador da serie a duplicar.</param>
    [HttpPost("series/{idSerie}/duplicar")]
    [ProducesResponseType(typeof(SerieFichaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DuplicarSerie(int idSerie)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _serieService.DuplicarSerieAsync(idSerie, codUsuario);
        return CreatedAtAction(nameof(Listar), new { id = resultado.CodFicha }, resultado);
    }

    /// <summary>
    /// Atualiza os par?metros f?sicos de uma s?rie de treino espec?fica.
    /// </summary>
    /// <param name="idSerie">C?digo identificador da s?rie espec?fica.</param>
    /// <param name="dto">Novas especifica??es para a s?rie.</param>
    [HttpPut("series/{idSerie}")]
    [ProducesResponseType(typeof(SerieFichaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AtualizarSerie(int idSerie, [FromBody] SerieFichaRequestDto dto)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _serieService.AtualizarSerieAsync(idSerie, codUsuario, dto);
        return Ok(resultado);
    }

    /// <summary>
    /// Remove uma s?rie da Ficha Base e reorganiza automaticamente a ordem de execu??o das s?ries restantes.
    /// </summary>
    /// <param name="idSerie">C?digo identificador da s?rie a ser removida.</param>
    [HttpDelete("series/{idSerie}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoverSerie(int idSerie)
    {
        var codUsuario = User.ObterUsuarioId();
        await _serieService.ExcluirSerieAsync(idSerie, codUsuario);
        return NoContent();
    }
}