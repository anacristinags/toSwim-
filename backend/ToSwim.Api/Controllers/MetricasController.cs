using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Metricas;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Enums;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

[Authorize]
[ApiController]
[Route("")]
public class MetricasController : ControllerBase
{
    private readonly IMetricasService _metricasService;

    public MetricasController(IMetricasService metricasService)
    {
        _metricasService = metricasService;
    }

    [HttpGet("dashboard/resumo")]
    public async Task<ActionResult<DashboardResumoDto>> ObterResumo()
    {
        int codUsuario = User.ObterUsuarioId();
        var resumo = await _metricasService.ObterResumoDashboardAsync(codUsuario);
        return Ok(resumo);
    }

    [HttpGet("metricas/pace-medio")]
    public async Task<ActionResult<IEnumerable<EvolucaoPaceDto>>> ObterEvolucaoPace([FromQuery] TipoNado? nado)
    {
        int codUsuario = User.ObterUsuarioId();
        var evolucao = await _metricasService.ObterEvolucaoPaceAsync(codUsuario, nado);
        return Ok(evolucao);
    }

    [HttpGet("metricas/melhores-tempos")]
    public async Task<ActionResult<IEnumerable<RecordePessoalDto>>> ObterRecordes([FromQuery] TipoNado? nado, [FromQuery] int? distancia)
    {
        int codUsuario = User.ObterUsuarioId();
        var recordes = await _metricasService.ObterMelhoresTemposAsync(codUsuario, nado, distancia);
        return Ok(recordes);
    }

    [HttpGet("metas/{id:int}/progresso")]
    public async Task<ActionResult<ProgressoMetaDto>> ObterProgressoMeta(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var progresso = await _metricasService.ObterProgressoMetaAsync(id, codUsuario);
        return Ok(progresso);
    }
}