using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.FichaBase;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável pela gestão das séries de exercícios vinculadas às Fichas Base.
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
    /// Lista todas as séries de exercícios que compõem uma determinada ficha base.
    /// </summary>
    /// <param name="id">Código identificador da Ficha Base.</param>
    [HttpGet("{id}/series")]
    [ProducesResponseType(typeof(IEnumerable<SerieFichaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(int id)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _serieService.ListarSeriesPorFichaAsync(id, codUsuario);
        return Ok(resultado);
    }

    /// <summary>
    /// Adiciona uma nova série de exercício física (nado, distância, repetições, pausa) na ficha de natação.
    /// </summary>
    /// <param name="id">Código identificador da Ficha Base.</param>
    /// <param name="dto">Informações físicas da série planejada.</param>
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
    /// Atualiza os parâmetros físicos de uma série de treino específica.
    /// </summary>
    /// <param name="idSerie">Código identificador da série específica.</param>
    /// <param name="dto">Novas especificações para a série.</param>
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
    /// Remove uma série da Ficha Base e reorganiza automaticamente a ordem de execução das séries restantes.
    /// </summary>
    /// <param name="idSerie">Código identificador da série a ser removida.</param>
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