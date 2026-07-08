using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Repositories.Interfaces;

public interface IConfigPiscinaRepository
{
    Task<ConfigPiscina?> BuscarPorUsuarioAsync(int codUsuario);
    Task<ConfigPiscina> CriarAsync(ConfigPiscina configPiscina);
    Task<ConfigPiscina> AtualizarAsync(ConfigPiscina configPiscina);
    Task<bool> ExistePorUsuarioAsync(int codUsuario);
}