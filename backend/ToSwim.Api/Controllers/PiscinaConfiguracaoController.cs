using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.ConfigPiscina;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

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

    /// <summary>Cria a configuração de piscina do usuário autenticado</summary>
    [HttpPost]
    [ProducesResponseType(typeof(ConfigPiscinaResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] ConfigPiscinaRequestDto dto)
    {
        var id = User.ObterUsuarioId();
        var resultado = await _service.CriarAsync(id, dto);
        return CreatedAtAction(nameof(BuscarMinha), resultado);
    }

    /// <summary>Retorna a configuração de piscina do usuário autenticado</summary>
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

    /// <summary>Atualiza a configuração de piscina do usuário autenticado</summary>
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