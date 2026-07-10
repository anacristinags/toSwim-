using System.Collections.Generic;
using System.Threading.Tasks;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;

namespace ToSwim.Infrastructure.Repositories.Interfaces;

public interface IMetaRepository
{
    Task<Meta?> ObterPorIdAsync(int codMeta, int codUsuario);
    Task<IEnumerable<Meta>> ObterTodasPorUsuarioAsync(int codUsuario, StatusMeta? status);
    Task AdicionarAsync(Meta meta);
    void Atualizar(Meta meta);
    void Remover(Meta meta);
    Task<bool> ExisteMetaAtivaParaSerieAsync(int codUsuario, int codSerieFicha);
    Task<bool> SalvarAlteracoesAsync();
}