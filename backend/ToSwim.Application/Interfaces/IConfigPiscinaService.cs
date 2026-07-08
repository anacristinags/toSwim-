using ToSwim.Application.DTOs.ConfigPiscina;

namespace ToSwim.Application.Interfaces;

public interface IConfigPiscinaService
{
    Task<ConfigPiscinaResponseDto?> BuscarPorUsuarioAsync(int codUsuario);
    Task<ConfigPiscinaResponseDto> CriarAsync(int codUsuario, ConfigPiscinaRequestDto dto);
    Task<ConfigPiscinaResponseDto> AtualizarAsync(int codUsuario, ConfigPiscinaRequestDto dto);
}