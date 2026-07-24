using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Treino;
using ToSwim.Application.DTOs.SerieTreino;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Enums;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável por gerenciar a execução dos treinos diários e suas séries realizadas.
/// </summary>
[Authorize]
[ApiController]
[Route("treinos")]
public class TreinosController : ControllerBase
{
    private readonly ITreinoService _treinoService;

    public TreinosController(ITreinoService treinoService)
    {
        _treinoService = treinoService;
    }

    /// <summary>
    /// Inicia a execução de um novo treino carregando a estrutura de uma Ficha Base existente.
    /// </summary>
    /// <remarks>
    /// A estrutura original da ficha é clonada para proteger o histórico de alterações futuras.
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult<TreinoResponseDto>> Iniciar([FromBody] IniciarTreinoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var novoTreino = await _treinoService.IniciarTreinoAsync(codUsuario, dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = novoTreino.CodTreino }, novoTreino);
    }

    /// <summary>
    /// Lista os treinos em andamento ou finalizados do nadador autenticado (com paginação).
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TreinoResponseDto>>> Listar(
        [FromQuery] StatusTreino? status,
        [FromQuery] int pagina = 1,
        [FromQuery] int tamanhoPagina = 20)
    {
        int codUsuario = User.ObterUsuarioId();
        var treinos = await _treinoService.ObterTodosPorUsuarioAsync(codUsuario, status, pagina, tamanhoPagina);
        return Ok(treinos);
    }

    /// <summary>
    /// Retorna os detalhes de um treino específico, incluindo suas séries.
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<TreinoResponseDto>> ObterPorId(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var treino = await _treinoService.ObterPorIdAsync(id, codUsuario);
        return Ok(treino);
    }

    /// <summary>
    /// Atualiza os dados do cabeçalho de um treino que está em andamento.
    /// </summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<TreinoResponseDto>> Atualizar(int id, [FromBody] AtualizarTreinoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var treinoAtualizado = await _treinoService.AtualizarTreinoAsync(id, codUsuario, dto);
        return Ok(treinoAtualizado);
    }

    /// <summary>
    /// Conclui o treino consolidando automaticamente as distâncias, tempos totais e ritmos médios de todas as séries.
    /// </summary>
    [HttpPut("{id:int}/finalizar")]
    public async Task<ActionResult<TreinoResponseDto>> Finalizar(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var treinoFinalizado = await _treinoService.FinalizarTreinoAsync(id, codUsuario);
        return Ok(treinoFinalizado);
    }

    /// <summary>
    /// Cancela o treino do dia.
    /// </summary>
    [HttpPut("{id:int}/cancelar")]
    public async Task<ActionResult<TreinoResponseDto>> Cancelar(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var treinoCancelado = await _treinoService.CancelarTreinoAsync(id, codUsuario);
        return Ok(treinoCancelado);
    }

    /// <summary>
    /// Adiciona uma nova série de treino realizada de forma avulsa (fora do padrão inicial da ficha).
    /// </summary>
    [HttpPost("{id:int}/series")]
    public async Task<ActionResult<SerieTreinoResponseDto>> AdicionarSerie(int id, [FromBody] SerieTreinoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var novaSerie = await _treinoService.AdicionarSerieAvulsaAsync(id, codUsuario, dto);
        return CreatedAtAction(nameof(ObterSeriePorId), new { id, idSerie = novaSerie.CodSerieTreino }, novaSerie);
    }

    /// <summary>
    /// Lista as séries vinculadas ao treino informado.
    /// </summary>
    [HttpGet("{id:int}/series")]
    public async Task<ActionResult<IEnumerable<SerieTreinoResponseDto>>> ListarSeries(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var series = await _treinoService.ListarSeriesPorTreinoAsync(id, codUsuario);
        return Ok(series);
    }

    /// <summary>
    /// Retorna o detalhe de uma série executada específica do treino.
    /// </summary>
    [HttpGet("{id:int}/series/{idSerie:int}")]
    public async Task<ActionResult<SerieTreinoResponseDto>> ObterSeriePorId(int id, int idSerie)
    {
        int codUsuario = User.ObterUsuarioId();
        var serie = await _treinoService.ObterSerieExecutadaPorIdAsync(idSerie, codUsuario);
        return Ok(serie);
    }

    /// <summary>
    /// Atualiza os parâmetros físicos ou anotações de uma série de treino específica.
    /// </summary>
    [HttpPut("{id:int}/series/{idSerie:int}")]
    public async Task<ActionResult<SerieTreinoResponseDto>> AtualizarSerie(int id, int idSerie, [FromBody] SerieTreinoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var serieAtualizada = await _treinoService.AtualizarSerieExecutadaAsync(idSerie, codUsuario, dto);
        return Ok(serieAtualizada);
    }

    /// <summary>
    /// Remove uma série executada do treino.
    /// </summary>
    [HttpDelete("{id:int}/series/{idSerie:int}")]
    public async Task<IActionResult> RemoverSerie(int id, int idSerie)
    {
        int codUsuario = User.ObterUsuarioId();
        await _treinoService.RemoverSerieExecutadaAsync(idSerie, codUsuario);
        return NoContent();
    }
}