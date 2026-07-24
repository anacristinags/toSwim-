using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.TreinoMeta;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

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

    [HttpPost("treinos/{idTreino:int}/metas/{idMeta:int}")]
    public async Task<ActionResult<TreinoMetaResponseDto>> Vincular(int idTreino, int idMeta, [FromQuery] int? codSerieTreino)
    {
        int codUsuario = User.ObterUsuarioId();
        var vinculo = await _treinoMetaService.VincularTreinoMetaAsync(idTreino, idMeta, codSerieTreino, codUsuario);
        return Ok(vinculo);
    }

    [HttpDelete("treinos/{idTreino:int}/metas/{idMeta:int}")]
    public async Task<IActionResult> Desvincular(int idTreino, int idMeta, [FromQuery] int? codSerieTreino)
    {
        int codUsuario = User.ObterUsuarioId();
        await _treinoMetaService.DesvincularTreinoMetaAsync(idTreino, idMeta, codSerieTreino, codUsuario);
        return NoContent();
    }

    [HttpGet("treinos/{idTreino:int}/metas")]
    public async Task<ActionResult<IEnumerable<TreinoMetaResponseDto>>> ListarMetasDoTreino(int idTreino)
    {
        int codUsuario = User.ObterUsuarioId();
        var metas = await _treinoMetaService.ListarMetasDoTreinoAsync(idTreino, codUsuario);
        return Ok(metas);
    }

    [HttpGet("metas/{idMeta:int}/treinos")]
    public async Task<ActionResult<IEnumerable<TreinoMetaResponseDto>>> ListarTreinosDaMeta(int idMeta)
    {
        int codUsuario = User.ObterUsuarioId();
        var treinos = await _treinoMetaService.ListarTreinosDaMetaAsync(idMeta, codUsuario);
        return Ok(treinos);
    }
}