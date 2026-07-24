using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToSwim.Application.DTOs.FichaBase;
using ToSwim.Application.Interfaces;
using ToSwim.Api.Extensions;

namespace ToSwim.Api.Controllers;

/// <summary>
/// Controller responsável pelo gerenciamento de Fichas Base utilizadas como gabarito para os treinos.
/// </summary>
[ApiController]
[Route("fichas-base")]
[Authorize]
public class FichasBaseController : ControllerBase
{
    private readonly IFichaBaseService _fichaService;

    public FichasBaseController(IFichaBaseService fichaService)
    {
        _fichaService = fichaService;
    }

    /// <summary>
    /// Lista as fichas bases ativas do nadador autenticado.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FichaBaseResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar()
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.ListarPorUsuarioAsync(codUsuario);
        return Ok(resultado);
    }

    /// <summary>
    /// Retorna os detalhes de uma ficha base específica do atleta com a listagem de suas séries.
    /// </summary>
    /// <param name="id">Código identificador da Ficha Base.</param>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(FichaBaseResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.BuscarPorIdAsync(id, codUsuario);
        return Ok(resultado);
    }

    /// <summary>
    /// Cria uma nova ficha base de treino de natação para o nadador.
    /// </summary>
    /// <remarks>
    /// Regra de Negócio: Existe um limite de no máximo 5 fichas base ativas por usuário cadastrado.
    /// </remarks>
    /// <param name="dto">Dados para a criação da ficha base de treino.</param>
    [HttpPost]
    [ProducesResponseType(typeof(FichaBaseResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] FichaBaseRequestDto dto)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.CriarAsync(codUsuario, dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = resultado.CodFicha }, resultado);
    }

    /// <summary>
    /// Atualiza o cabeçalho de uma ficha base de treino existente.
    /// </summary>
    /// <param name="id">Código identificador da ficha.</param>
    /// <param name="dto">Dados atualizados da ficha de natação.</param>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(FichaBaseResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(int id, [FromBody] FichaBaseRequestDto dto)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.AtualizarAsync(id, codUsuario, dto);
        return Ok(resultado);
    }

    /// <summary>
    /// Altera o status operacional da ficha base de treino (1 = Ativo, 0 = Inativo).
    /// </summary>
    /// <param name="id">Código identificador da ficha.</param>
    /// <param name="novoStatus">Novo status desejado para o registro.</param>
    [HttpPut("{id}/status")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AlterarStatus(int id, [FromBody] short novoStatus)
    {
        var codUsuario = User.ObterUsuarioId();
        await _fichaService.AlterarStatusAsync(id, codUsuario, novoStatus);
        return NoContent();
    }

    /// <summary>
    /// Inativa ou deleta fisicamente uma Ficha Base.
    /// </summary>
    /// <remarks>
    /// Se a ficha possuir treinos vinculados em seu histórico, ela é inativada logicamente (status = 0). Do contrário, é executado o DELETE físico.
    /// </remarks>
    /// <param name="id">Código identificador da ficha base.</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(int id)
    {
        var codUsuario = User.ObterUsuarioId();
        await _fichaService.DeletarAsync(id, codUsuario);
        return NoContent();
    }

    /// <summary>
    /// Duplica uma Ficha Base existente criando uma cópia com a identificação '(Cópia)' e copiando todas as suas séries.
    /// </summary>
    /// <remarks>
    /// Útil para criar novos treinos semelhantes sem violar a regra de limite de 5 fichas ativas.
    /// </remarks>
    /// <param name="id">Código identificador da Ficha de origem.</param>
    [HttpPost("{id}/duplicar")]
    [ProducesResponseType(typeof(FichaBaseResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Duplicar(int id)
    {
        var codUsuario = User.ObterUsuarioId();
        var resultado = await _fichaService.DuplicarAsync(id, codUsuario);
        return CreatedAtAction(nameof(ObterPorId), new { id = resultado.CodFicha }, resultado);
    }
}