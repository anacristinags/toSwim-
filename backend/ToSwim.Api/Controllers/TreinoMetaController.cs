using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.TreinoMeta;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável por criar vínculos analíticos entre execuções reais de treino/série e as metas estabelecidas.
/// </summary>
[Authorize]
[ApiController]
[Route("")]
public class TreinoMetaController : ControllerBase
{
    private readonly ITreinoMetaService _treinoMetaService;

    public TreinoMetaController(ITreinoMetaService treinoMetaService)
    {
        _treinoMetaService = treinoMetaService;
    }

    /// <summary>
    /// Vincula a execução de um treino real ou de uma série realizada à meta de tempo correspondente.
    /// </summary>
    /// <param name="idTreino">Código identificador do treino.</param>
    /// <param name="idMeta">Código identificador da meta.</param>
    /// <param name="codSerieTreino">Filtro opcional para associar a uma série do treino específica.</param>
    [HttpPost("treinos/{idTreino:int}/metas/{idMeta:int}")]
    public async Task<ActionResult<TreinoMetaResponseDto>> Vincular(int idTreino, int idMeta, [FromQuery] int? codSerieTreino)
    {
        int codUsuario = User.ObterUsuarioId();
        var vinculo = await _treinoMetaService.VincularTreinoMetaAsync(idTreino, idMeta, codSerieTreino, codUsuario);
        return Ok(vinculo);
    }

    /// <summary>
    /// Remove o vínculo entre o treino/série e a meta correspondente.
    /// </summary>
    [HttpDelete("treinos/{idTreino:int}/metas/{idMeta:int}")]
    public async Task<IActionResult> Desvincular(int idTreino, int idMeta, [FromQuery] int? codSerieTreino)
    {
        int codUsuario = User.ObterUsuarioId();
        await _treinoMetaService.DesvincularTreinoMetaAsync(idTreino, idMeta, codSerieTreino, codUsuario);
        return NoContent();
    }

    /// <summary>
    /// Lista todos os vínculos de metas associados àquele treino do dia.
    /// </summary>
    [HttpGet("treinos/{idTreino:int}/metas")]
    public async Task<ActionResult<IEnumerable<TreinoMetaResponseDto>>> ListarMetasDoTreino(int idTreino)
    {
        int codUsuario = User.ObterUsuarioId();
        var metas = await _treinoMetaService.ListarMetasDoTreinoAsync(idTreino, codUsuario);
        return Ok(metas);
    }

    /// <summary>
    /// Retorna o histórico de todas as execuções de treinos vinculadas ao avanço daquela meta de tempo.
    /// </summary>
    [HttpGet("metas/{idMeta:int}/treinos")]
    public async Task<ActionResult<IEnumerable<TreinoMetaResponseDto>>> ListarTreinosDaMeta(int idMeta)
    {
        int codUsuario = User.ObterUsuarioId();
        var treinos = await _treinoMetaService.ListarTreinosDaMetaAsync(idMeta, codUsuario);
        return Ok(treinos);
    }
}