using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.ConfigPiscina;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável por gerenciar as dimensões e parâmetros de contagem da piscina do atleta.
/// </summary>
[ApiController]
[Route("piscina-configuracao")]
[Authorize]
public class PiscinaConfiguracaoController : ControllerBase
{
    private readonly IConfigPiscinaService _service;

    public PiscinaConfiguracaoController(IConfigPiscinaService service)
    {
        _service = service;
    }

    /// <summary>
    /// Cria as configurações de piscina (tamanho em metros e forma de contagem) para o usuário autenticado.
    /// </summary>
    /// <remarks>
    /// Regra de Negócio: Cada atleta possui apenas 1 configuração de piscina cadastrada no sistema.
    /// </remarks>
    /// <param name="dto">Modelo contendo as dimensões (25m ou 50m) e a forma de contagem (distância ou voltas).</param>
    [HttpPost]
    [ProducesResponseType(typeof(ConfigPiscinaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] ConfigPiscinaRequestDto dto)
    {
        var id = User.ObterUsuarioId();
        var resultado = await _service.CriarAsync(id, dto);
        return CreatedAtAction(nameof(BuscarMinha), resultado);
    }

    /// <summary>
    /// Retorna as configurações de piscina associadas ao perfil do atleta autenticado.
    /// </summary>
    [HttpGet("me")]
    [ProducesResponseType(typeof(ConfigPiscinaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarMinha()
    {
        var id = User.ObterUsuarioId();
        var resultado = await _service.BuscarPorUsuarioAsync(id);

        if (resultado is null)
            return NotFound(new { erro = "Configuração de piscina não encontrada" });

        return Ok(resultado);
    }

    /// <summary>
    /// Atualiza as configurações de piscina do atleta autenticado.
    /// </summary>
    /// <param name="dto">Parâmetros de dimensões e contagem atualizados.</param>
    [HttpPut("me")]
    [ProducesResponseType(typeof(ConfigPiscinaResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar([FromBody] ConfigPiscinaRequestDto dto)
    {
        var id = User.ObterUsuarioId();
        var resultado = await _service.AtualizarAsync(id, dto);
        return Ok(resultado);
    }
}