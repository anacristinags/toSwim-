using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Repositories.Interfaces;

public interface IUsuarioRepository
{
    Task<Usuario?> BuscarPorEmailAsync(string email);
    Task<Usuario?> BuscarPorIdAsync(int id);
    Task<bool> EmailExisteAsync(string email);
    Task<Usuario> CriarAsync(Usuario usuario);
    Task<Usuario> AtualizarAsync(Usuario usuario);
}