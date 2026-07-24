using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Treino;
using ToSwim.Application.DTOs.SerieTreino;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Enums;
using ToSwim.Api.Extensions; // Para ler o ID do usuário autenticado no token

namespace ToSwim.Api.Controllers;

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

    // ==========================================
    // 5. EXECUÇÃO DE TREINOS (/treinos)
    // ==========================================

    [HttpPost]
    public async Task<ActionResult<TreinoResponseDto>> Iniciar([FromBody] IniciarTreinoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var novoTreino = await _treinoService.IniciarTreinoAsync(codUsuario, dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = novoTreino.CodTreino }, novoTreino);
    }

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

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TreinoResponseDto>> ObterPorId(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var treino = await _treinoService.ObterPorIdAsync(id, codUsuario);
        return Ok(treino);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<TreinoResponseDto>> Atualizar(int id, [FromBody] AtualizarTreinoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var treinoAtualizado = await _treinoService.AtualizarTreinoAsync(id, codUsuario, dto);
        return Ok(treinoAtualizado);
    }

    [HttpPut("{id:int}/finalizar")]
    public async Task<ActionResult<TreinoResponseDto>> Finalizar(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var treinoFinalizado = await _treinoService.FinalizarTreinoAsync(id, codUsuario);
        return Ok(treinoFinalizado);
    }

    [HttpPut("{id:int}/cancelar")]
    public async Task<ActionResult<TreinoResponseDto>> Cancelar(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var treinoCancelado = await _treinoService.CancelarTreinoAsync(id, codUsuario);
        return Ok(treinoCancelado);
    }

    // ==========================================
    // 6. SÉRIES DO TREINO EXECUTADO (/treinos/{id}/series)
    // ==========================================

    [HttpPost("{id:int}/series")]
    public async Task<ActionResult<SerieTreinoResponseDto>> AdicionarSerie(int id, [FromBody] SerieTreinoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var novaSerie = await _treinoService.AdicionarSerieAvulsaAsync(id, codUsuario, dto);
        return CreatedAtAction(nameof(ObterSeriePorId), new { id, idSerie = novaSerie.CodSerieTreino }, novaSerie);
    }

    [HttpGet("{id:int}/series")]
    public async Task<ActionResult<IEnumerable<SerieTreinoResponseDto>>> ListarSeries(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var series = await _treinoService.ListarSeriesPorTreinoAsync(id, codUsuario);
        return Ok(series);
    }

    [HttpGet("{id:int}/series/{idSerie:int}")]
    public async Task<ActionResult<SerieTreinoResponseDto>> ObterSeriePorId(int id, int idSerie)
    {
        int codUsuario = User.ObterUsuarioId();
        var serie = await _treinoService.ObterSerieExecutadaPorIdAsync(idSerie, codUsuario);
        return Ok(serie);
    }

    [HttpPut("{id:int}/series/{idSerie:int}")]
    public async Task<ActionResult<SerieTreinoResponseDto>> AtualizarSerie(int id, int idSerie, [FromBody] SerieTreinoRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var serieAtualizada = await _treinoService.AtualizarSerieExecutadaAsync(idSerie, codUsuario, dto);
        return Ok(serieAtualizada);
    }

    [HttpDelete("{id:int}/series/{idSerie:int}")]
    public async Task<IActionResult> RemoverSerie(int id, int idSerie)
    {
        int codUsuario = User.ObterUsuarioId();
        await _treinoService.RemoverSerieExecutadaAsync(idSerie, codUsuario);
        return NoContent();
    }
}