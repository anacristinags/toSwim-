using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Repeticao;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável pelo controle fino das repetições/tiros individuais de cada série realizada.
/// </summary>
[Authorize]
[ApiController]
[Route("treinos/{idTreino:int}/series/{idSerie:int}/repeticoes")]
public class RepeticoesController : ControllerBase
{
    private readonly IRepeticaoSerieTreinoService _repeticaoService;

    public RepeticoesController(IRepeticaoSerieTreinoService repeticaoService)
    {
        _repeticaoService = repeticaoService;
    }

    /// <summary>
    /// Registra uma nova repetição de tempo (volta ou tiro) na série de treino diário.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RepeticaoResponseDto>> Registrar(int idTreino, int idSerie, [FromBody] RepeticaoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var novaRepeticao = await _repeticaoService.RegistrarRepeticaoAsync(idTreino, idSerie, codUsuario, dto);
        return CreatedAtAction(
            nameof(ObterPorId),
            new { idTreino, idSerie, idRep = novaRepeticao.CodRepeticaoSerieTreino },
            novaRepeticao);
    }

    /// <summary>
    /// Lista todos os tiros/voltas individuais registrados para a série.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RepeticaoResponseDto>>> Listar(int idTreino, int idSerie)
    {
        int codUsuario = User.ObterUsuarioId();
        var repeticoes = await _repeticaoService.ListarRepeticoesDaSerieAsync(idTreino, idSerie, codUsuario);
        return Ok(repeticoes);
    }

    /// <summary>
    /// Retorna os dados de uma repetição de tiro específica.
    /// </summary>
    [HttpGet("{idRep:int}")]
    public async Task<ActionResult<RepeticaoResponseDto>> ObterPorId(int idTreino, int idSerie, int idRep)
    {
        int codUsuario = User.ObterUsuarioId();
        var repeticao = await _repeticaoService.ObterPorIdAsync(idTreino, idSerie, idRep, codUsuario);
        return Ok(repeticao);
    }

    /// <summary>
    /// Atualiza os valores de distância ou tempo de uma repetição/tiro específico.
    /// </summary>
    [HttpPut("{idRep:int}")]
    public async Task<ActionResult<RepeticaoResponseDto>> Atualizar(int idTreino, int idSerie, int idRep, [FromBody] RepeticaoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var repeticaoAtualizada = await _repeticaoService.AtualizarRepeticaoAsync(idTreino, idSerie, idRep, codUsuario, dto);
        return Ok(repeticaoAtualizada);
    }

    /// <summary>
    /// Remove o registro de uma repetição/tiro do sistema.
    /// </summary>
    [HttpDelete("{idRep:int}")]
    public async Task<IActionResult> Remover(int idTreino, int idSerie, int idRep)
    {
        int codUsuario = User.ObterUsuarioId();
        await _repeticaoService.RemoverRepeticaoAsync(idTreino, idSerie, idRep, codUsuario);
        return NoContent();
    }
}