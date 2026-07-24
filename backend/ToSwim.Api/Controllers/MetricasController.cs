using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Metricas;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Enums;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável por consolidar estatísticas, recordes pessoais e gráficos de desempenho do atleta.
/// </summary>
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

    /// <summary>
    /// Retorna o resumo operacional do nadador para exibição dos cards do Dashboard inicial.
    /// </summary>
    [HttpGet("dashboard/resumo")]
    public async Task<ActionResult<DashboardResumoDto>> ObterResumo()
    {
        int codUsuario = User.ObterUsuarioId();
        var resumo = await _metricasService.ObterResumoDashboardAsync(codUsuario);
        return Ok(resumo);
    }

    /// <summary>
    /// Retorna os dados para geração do gráfico de linha com a evolução histórica de pace do nadador.
    /// </summary>
    /// <param name="nado">Filtro opcional para exibir a evolução de apenas um tipo de nado específico.</param>
    [HttpGet("metricas/pace-medio")]
    public async Task<ActionResult<IEnumerable<EvolucaoPaceDto>>> ObterEvolucaoPace([FromQuery] TipoNado? nado)
    {
        int codUsuario = User.ObterUsuarioId();
        var evolucao = await _metricasService.ObterEvolucaoPaceAsync(codUsuario, nado);
        return Ok(evolucao);
    }

    /// <summary>
    /// Retorna as melhores marcas pessoais (Recordes) do nadador por distância e tipo de nado.
    /// </summary>
    [HttpGet("metricas/melhores-tempos")]
    public async Task<ActionResult<IEnumerable<RecordePessoalDto>>> ObterRecordes([FromQuery] TipoNado? nado, [FromQuery] int? distancia)
    {
        int codUsuario = User.ObterUsuarioId();
        var recordes = await _metricasService.ObterMelhoresTemposAsync(codUsuario, nado, distancia);
        return Ok(recordes);
    }

    /// <summary>
    /// Retorna o andamento gráfico detalhado do progresso de aproximação de uma meta de tempo específica.
    /// </summary>
    [HttpGet("metas/{id:int}/progresso")]
    public async Task<ActionResult<ProgressoMetaDto>> ObterProgressoMeta(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var progresso = await _metricasService.ObterProgressoMetaAsync(id, codUsuario);
        return Ok(progresso);
    }
}