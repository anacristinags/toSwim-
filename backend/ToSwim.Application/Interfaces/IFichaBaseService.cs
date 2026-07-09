using ToSwim.Application.DTOs.FichaBase;

namespace ToSwim.Application.Interfaces;

public interface IFichaBaseService
{
    Task<IEnumerable<FichaBaseResponseDto>> ListarPorUsuarioAsync(int codUsuario);
    Task<FichaBaseResponseDto> BuscarPorIdAsync(int codFicha, int codUsuario);
    Task<FichaBaseResponseDto> CriarAsync(int codUsuario, FichaBaseRequestDto dto);
    Task<FichaBaseResponseDto> AtualizarAsync(int codFicha, int codUsuario, FichaBaseRequestDto dto);
    Task AlterarStatusAsync(int codFicha, int codUsuario, short novoStatus);
    Task DeletarAsync(int codFicha, int codUsuario);
    Task<FichaBaseResponseDto> DuplicarAsync(int codFicha, int codUsuario);
}