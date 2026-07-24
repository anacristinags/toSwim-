using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Repositories.Interfaces;

public interface ITreinoMetaRepository
{
    Task<TreinoMeta?> ObterVinculoAsync(int codTreino, int codMeta, int? codSerieTreino);
    Task<IEnumerable<TreinoMeta>> ListarMetasPorTreinoAsync(int codTreino, int codUsuario);
    Task<IEnumerable<TreinoMeta>> ListarTreinosPorMetaAsync(int codMeta, int codUsuario);
    Task AdicionarAsync(TreinoMeta vinculo);
    void Remover(TreinoMeta vinculo);
    Task<bool> ExisteVinculoAsync(int codTreino, int codMeta, int? codSerieTreino);
    Task<bool> SalvarAlteracoesAsync();
}