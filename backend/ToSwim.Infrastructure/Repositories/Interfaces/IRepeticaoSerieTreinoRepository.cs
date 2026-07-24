using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Repositories.Interfaces;

public interface IRepeticaoSerieTreinoRepository
{
    Task<RepeticaoSerieTreino?> ObterPorIdAsync(int codRepeticao, int codUsuario);
    Task<IEnumerable<RepeticaoSerieTreino>> ObterPorSerieTreinoAsync(int codSerieTreino, int codUsuario);
    Task AdicionarAsync(RepeticaoSerieTreino repeticao);
    void Atualizar(RepeticaoSerieTreino repeticao);
    void Remover(RepeticaoSerieTreino repeticao);
    Task<bool> ExisteNumeroRepeticaoNaSerieAsync(int codSerieTreino, short numeroRepeticao);
    Task<bool> SalvarAlteracoesAsync();
}