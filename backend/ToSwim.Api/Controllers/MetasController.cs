using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Meta;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Enums;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável pela gestão das Metas de Tempo dos nadadores.
/// </summary>
[Authorize]
[ApiController]
[Route("metas")]
public class MetasController : ControllerBase
{
    private readonly IMetaService _metaService;

    public MetasController(IMetaService metaService)
    {
        _metaService = metaService;
    }

    /// <summary>
    /// Retorna todas as metas cadastradas para o usuário autenticado.
    /// </summary>
    /// <param name="status">Filtro opcional para o status da meta (0 = Ativa, 1 = Concluída, 2 = Cancelada).</param>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MetaResponseDto>>> ObterMetas([FromQuery] StatusMeta? status)
    {
        int codUsuario = User.ObterUsuarioId();
        var metas = await _metaService.ObterTodasPorUsuarioAsync(codUsuario, status);
        return Ok(metas);
    }

    /// <summary>
    /// Retorna os detalhes de uma meta de tempo específica com base em seu ID.
    /// </summary>
    /// <param name="id">Código identificador da meta de tempo.</param>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MetaResponseDto>> ObterPorId(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var meta = await _metaService.ObterPorIdAsync(id, codUsuario);
        return Ok(meta);
    }

    /// <summary>
    /// Cria uma nova meta de tempo vinculada a uma série de ficha base específica.
    /// </summary>
    /// <remarks>
    /// O pace alvo é calculado automaticamente com base na distância e tempo informados.
    /// </remarks>
    [HttpPost]
    public async Task<ActionResult<MetaResponseDto>> Criar([FromBody] MetaRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var novaMeta = await _metaService.CriarMetaAsync(codUsuario, dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = novaMeta.CodMeta }, novaMeta);
    }

    /// <summary>
    /// Atualiza os dados de uma meta de tempo ativa.
    /// </summary>
    /// <param name="id">Código identificador da meta.</param>
    /// <param name="dto">Novos dados para atualização da meta.</param>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<MetaResponseDto>> Atualizar(int id, [FromBody] MetaRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var metaAtualizada = await _metaService.AtualizarMetaAsync(id, codUsuario, dto);
        return Ok(metaAtualizada);
    }

    /// <summary>
    /// Altera o status de uma meta de tempo (ex: Concluir ou Cancelar).
    /// </summary>
    /// <param name="id">Código identificador da meta.</param>
    /// <param name="status">Novo status para a meta (0 = Ativa, 1 = Concluída, 2 = Cancelada).</param>
    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<MetaResponseDto>> AtualizarStatus(int id, [FromQuery] StatusMeta status)
    {
        int codUsuario = User.ObterUsuarioId();
        var metaAtualizada = await _metaService.AtualizarStatusMetaAsync(id, codUsuario, status);
        return Ok(metaAtualizada);
    }

    /// <summary>
    /// Exclui uma meta de tempo do sistema.
    /// </summary>
    /// <param name="id">Código identificador da meta.</param>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        await _metaService.RemoverMetaAsync(id, codUsuario);
        return NoContent();
    }
}