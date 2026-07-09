using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Repositories.Interfaces;

public interface ISerieFichaRepository
{
    Task<SerieFicha?> BuscarPorIdAsync(int codSerieFicha);
    Task<bool> ExisteOrdemNaFichaAsync(int codFicha, short ordem);
    Task<SerieFicha> CriarAsync(SerieFicha serie);
    Task<SerieFicha> AtualizarAsync(SerieFicha serie);
    Task ExcluirAsync(SerieFicha serie);
    Task ReordenarSeriesFichaAsync(int codFicha);
}