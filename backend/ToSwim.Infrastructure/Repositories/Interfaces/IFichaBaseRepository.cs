using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Repositories.Interfaces;

public interface IFichaBaseRepository
{
    Task<IEnumerable<FichaBase>> ListarPorUsuarioAsync(int codUsuario);
    Task<FichaBase?> BuscarPorIdAsync(int codFicha, int codUsuario);
    Task<int> ContarFichasAtivasAsync(int codUsuario);
    Task<FichaBase> CriarAsync(FichaBase ficha);
    Task<FichaBase> AtualizarAsync(FichaBase ficha);
    Task DeletarLogicamenteAsync(FichaBase ficha);
    Task<bool> TemTreinosVinculadosAsync(int codFicha);
    Task ExcluirFisicamenteAsync(FichaBase ficha);
}