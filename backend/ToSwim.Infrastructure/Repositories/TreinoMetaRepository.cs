using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Infrastructure.Repositories;

public class TreinoMetaRepository : ITreinoMetaRepository
{
    private readonly ToSwimDbContext _context;

    public TreinoMetaRepository(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<TreinoMeta?> ObterVinculoAsync(int codTreino, int codMeta, int? codSerieTreino)
    {
        return await _context.TreinosMetas
            .FirstOrDefaultAsync(tm => tm.CodTreino == codTreino
                                      && tm.CodMeta == codMeta
                                      && tm.CodSerieTreino == codSerieTreino);
    }

    public async Task<IEnumerable<TreinoMeta>> ListarMetasPorTreinoAsync(int codTreino, int codUsuario)
    {
        return await _context.TreinosMetas
            .Include(tm => tm.Meta)
            .Include(tm => tm.SerieTreino)
            .Where(tm => tm.CodTreino == codTreino && tm.Treino!.CodUsuario == codUsuario)
            .ToListAsync();
    }

    public async Task<IEnumerable<TreinoMeta>> ListarTreinosPorMetaAsync(int codMeta, int codUsuario)
    {
        return await _context.TreinosMetas
            .Include(tm => tm.Treino)
            .Include(tm => tm.SerieTreino)
            .Where(tm => tm.CodMeta == codMeta && tm.Meta!.CodUsuario == codUsuario)
            .OrderByDescending(tm => tm.Treino!.DataTreino)
            .ToListAsync();
    }

    public async Task AdicionarAsync(TreinoMeta vinculo)
    {
        await _context.TreinosMetas.AddAsync(vinculo);
    }

    public void Remover(TreinoMeta vinculo)
    {
        _context.TreinosMetas.Remove(vinculo);
    }

    public async Task<bool> ExisteVinculoAsync(int codTreino, int codMeta, int? codSerieTreino)
    {
        return await _context.TreinosMetas
            .AnyAsync(tm => tm.CodTreino == codTreino
                           && tm.CodMeta == codMeta
                           && tm.CodSerieTreino == codSerieTreino);
    }

    public async Task<bool> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}