using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Application.DTOs.Treino;
using ToSwim.Application.DTOs.SerieTreino;
using ToSwim.Domain.Enums;

namespace ToSwim.Application.Interfaces;

public interface ITreinoService
{
    // Execução de Treino
    Task<TreinoResponseDto> IniciarTreinoAsync(int codUsuario, IniciarTreinoRequestDto dto);
    Task<TreinoResponseDto> ObterPorIdAsync(int codTreino, int codUsuario);
    Task<IEnumerable<TreinoResponseDto>> ObterTodosPorUsuarioAsync(int codUsuario, StatusTreino? status, int pagina, int tamanhoPagina);
    Task<TreinoResponseDto> AtualizarTreinoAsync(int codTreino, int codUsuario, AtualizarTreinoRequestDto dto);
    Task<TreinoResponseDto> FinalizarTreinoAsync(int codTreino, int codUsuario);
    Task<TreinoResponseDto> CancelarTreinoAsync(int codTreino, int codUsuario);

    // Séries do Treino
    Task<SerieTreinoResponseDto> AdicionarSerieAvulsaAsync(int codTreino, int codUsuario, SerieTreinoRequestDto dto);
    Task<IEnumerable<SerieTreinoResponseDto>> ListarSeriesPorTreinoAsync(int codTreino, int codUsuario);
    Task<SerieTreinoResponseDto> ObterSerieExecutadaPorIdAsync(int codSerieTreino, int codUsuario);
    Task<SerieTreinoResponseDto> AtualizarSerieExecutadaAsync(int codSerieTreino, int codUsuario, SerieTreinoRequestDto dto);
    Task RemoverSerieExecutadaAsync(int codSerieTreino, int codUsuario);
}