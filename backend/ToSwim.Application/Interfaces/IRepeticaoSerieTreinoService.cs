using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Application.DTOs.Repeticao;

namespace ToSwim.Application.Interfaces;

public interface IRepeticaoSerieTreinoService
{
    Task<RepeticaoResponseDto> RegistrarRepeticaoAsync(int codTreino, int codSerieTreino, int codUsuario, RepeticaoRequestDto dto);
    Task<IEnumerable<RepeticaoResponseDto>> ListarRepeticoesDaSerieAsync(int codTreino, int codSerieTreino, int codUsuario);
    Task<RepeticaoResponseDto> ObterPorIdAsync(int codTreino, int codSerieTreino, int codRepeticao, int codUsuario);
    Task<RepeticaoResponseDto> AtualizarRepeticaoAsync(int codTreino, int codSerieTreino, int codRepeticao, int codUsuario, RepeticaoRequestDto dto);
    Task RemoverRepeticaoAsync(int codTreino, int codSerieTreino, int codRepeticao, int codUsuario);
}