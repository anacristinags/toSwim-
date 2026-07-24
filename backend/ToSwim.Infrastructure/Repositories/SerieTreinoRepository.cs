using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Infrastructure.Repositories;

public class SerieTreinoRepository : ISerieTreinoRepository
{
    private readonly ToSwimDbContext _context;

    public SerieTreinoRepository(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<SerieTreino?> ObterPorIdAsync(int codSerieTreino, int codUsuario)
    {
        return await _context.SeriesTreino
            .Include(s => s.Treino)
            .FirstOrDefaultAsync(s => s.CodSerieTreino == codSerieTreino && s.Treino!.CodUsuario == codUsuario);
    }

    public async Task<IEnumerable<SerieTreino>> ObterSeriesPorTreinoAsync(int codTreino, int codUsuario)
    {
        return await _context.SeriesTreino
            .AsNoTracking()
            .Where(s => s.CodTreino == codTreino && s.Treino!.CodUsuario == codUsuario)
            .OrderBy(s => s.Ordem)
            .ToListAsync();
    }

    public async Task AdicionarAsync(SerieTreino serie)
    {
        await _context.SeriesTreino.AddAsync(serie);
    }

    public void Atualizar(SerieTreino serie)
    {
        _context.SeriesTreino.Update(serie);
    }

    public void Remover(SerieTreino serie)
    {
        _context.SeriesTreino.Remove(serie);
    }

    public async Task<bool> ExisteOrdemNoTreinoAsync(int codTreino, short ordem)
    {
        return await _context.SeriesTreino
            .AnyAsync(s => s.CodTreino == codTreino && s.Ordem == ordem);
    }

    public async Task<bool> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}