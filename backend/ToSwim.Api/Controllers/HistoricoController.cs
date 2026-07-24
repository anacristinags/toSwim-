using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Treino;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Enums;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável pelas consultas históricas de treinos antigos concluídos.
/// </summary>
[Authorize]
[ApiController]
[Route("historico/treinos")]
public class HistoricoController : ControllerBase
{
    private readonly ITreinoService _treinoService;

    public HistoricoController(ITreinoService treinoService)
    {
        _treinoService = treinoService;
    }

    /// <summary>
    /// Retorna o histórico paginado de treinos do nadador que foram marcados com status de Concluído.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TreinoResponseDto>>> ObterHistorico(
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        int codUsuario = User.ObterUsuarioId();
        // O histórico retorna especificamente execuções com StatusTreino.Concluido (1)
        var treinos = await _treinoService.ObterTodosPorUsuarioAsync(codUsuario, StatusTreino.Concluido, pagina, tamanhoPagina);
        return Ok(treinos);
    }

    /// <summary>
    /// Retorna o detalhamento físico (séries, distâncias e velocidades) de um treino antigo já consolidado.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TreinoResponseDto>> ObterDetalheHistorico(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var treino = await _treinoService.ObterPorIdAsync(id, codUsuario);

        if (treino.Status != StatusTreino.Concluido)
            return BadRequest("O treino requisitado ainda não foi finalizado.");

        return Ok(treino);
    }
}