using ToSwim.Application.DTOs.Usuario;

namespace ToSwim.Application.Interfaces;

public interface IUsuarioService
{
    Task<UsuarioResponseDto?> BuscarPorIdAsync(int id);
    Task AtualizarSenhaAsync(int id, AtualizarSenhaRequestDto dto);
}