using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;

namespace ToSwim.Infrastructure.Repositories.Interfaces;

public interface ITreinoRepository
{
    Task<Treino?> ObterPorIdAsync(int codTreino, int codUsuario);
    Task<IEnumerable<Treino>> ObterTodosPorUsuarioAsync(int codUsuario, StatusTreino? status, int pagina, int tamanhoPagina);
    Task AdicionarAsync(Treino treino);
    void Atualizar(Treino treino);
    Task<bool> SalvarAlteracoesAsync();
}