using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Repositories.Interfaces;

public interface ISerieTreinoRepository
{
    Task<SerieTreino?> ObterPorIdAsync(int codSerieTreino, int codUsuario);
    Task<IEnumerable<SerieTreino>> ObterSeriesPorTreinoAsync(int codTreino, int codUsuario);
    Task AdicionarAsync(SerieTreino serie);
    void Atualizar(SerieTreino serie);
    void Remover(SerieTreino serie);
    Task<bool> ExisteOrdemNoTreinoAsync(int codTreino, short ordem);
    Task<bool> SalvarAlteracoesAsync();
}