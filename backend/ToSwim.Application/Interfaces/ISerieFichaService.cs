using ToSwim.Application.DTOs.FichaBase;

namespace ToSwim.Application.Interfaces;

public interface ISerieFichaService
{
    Task<IEnumerable<SerieFichaResponseDto>> ListarSeriesPorFichaAsync(int codFicha, int codUsuario);
    Task<SerieFichaResponseDto> AdicionarSerieAsync(int codFicha, int codUsuario, SerieFichaRequestDto dto);
    Task<SerieFichaResponseDto> DuplicarSerieAsync(int codSerieFicha, int codUsuario);
    Task<SerieFichaResponseDto> AtualizarSerieAsync(int codSerieFicha, int codUsuario, SerieFichaRequestDto dto);
    Task ExcluirSerieAsync(int codSerieFicha, int codUsuario);
}
