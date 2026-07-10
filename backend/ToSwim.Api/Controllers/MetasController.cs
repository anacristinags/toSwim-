using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.Meta;
using ToSwim.Application.Interfaces;
using ToSwim.Domain.Enums;
using ToSwim.Api.Extensions; // Namespace do ClaimsPrincipalExtensions.ObterUsuarioId()

namespace ToSwim.Api.Controllers;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MetaResponseDto>>> ObterMetas([FromQuery] StatusMeta? status)
    {
        int codUsuario = User.ObterUsuarioId();
        var metas = await _metaService.ObterTodasPorUsuarioAsync(codUsuario, status);
        return Ok(metas);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MetaResponseDto>> ObterPorId(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        var meta = await _metaService.ObterPorIdAsync(id, codUsuario);
        return Ok(meta);
    }

    [HttpPost]
    public async Task<ActionResult<MetaResponseDto>> Criar([FromBody] MetaRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var novaMeta = await _metaService.CriarMetaAsync(codUsuario, dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = novaMeta.CodMeta }, novaMeta);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MetaResponseDto>> Atualizar(int id, [FromBody] MetaRequestDto dto)
    {
        int codUsuario = User.ObterUsuarioId();
        var metaAtualizada = await _metaService.AtualizarMetaAsync(id, codUsuario, dto);
        return Ok(metaAtualizada);
    }

    [HttpPut("{id:int}/status")]
    public async Task<ActionResult<MetaResponseDto>> AtualizarStatus(int id, [FromQuery] StatusMeta status)
    {
        int codUsuario = User.ObterUsuarioId();
        var metaAtualizada = await _metaService.AtualizarStatusMetaAsync(id, codUsuario, status);
        return Ok(metaAtualizada);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id)
    {
        int codUsuario = User.ObterUsuarioId();
        await _metaService.RemoverMetaAsync(id, codUsuario);
        return NoContent();
    }
}