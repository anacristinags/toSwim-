using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Infrastructure.Repositories;

public class SerieFichaRepository : ISerieFichaRepository
{
    private readonly ToSwimDbContext _context;

    public SerieFichaRepository(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<SerieFicha?> BuscarPorIdAsync(int codSerieFicha)
    {
        return await _context.SeriesFicha
            .Include(s => s.Ficha)
            .FirstOrDefaultAsync(s => s.CodSerieFicha == codSerieFicha);
    }

    public async Task<bool> ExisteOrdemNaFichaAsync(int codFicha, short ordem)
    {
        return await _context.SeriesFicha
            .AnyAsync(s => s.CodFicha == codFicha && s.Ordem == ordem);
    }

    public async Task<SerieFicha> CriarAsync(SerieFicha serie)
    {
        _context.SeriesFicha.Add(serie);
        await _context.SaveChangesAsync();
        return serie;
    }

    public async Task<SerieFicha> AtualizarAsync(SerieFicha serie)
    {
        _context.SeriesFicha.Update(serie);
        await _context.SaveChangesAsync();
        return serie;
    }

    public async Task ExcluirAsync(SerieFicha serie)
    {
        _context.SeriesFicha.Remove(serie);
        await _context.SaveChangesAsync();
    }

    public async Task ReordenarSeriesFichaAsync(int codFicha)
    {
        var series = await _context.SeriesFicha
            .Where(s => s.CodFicha == codFicha)
            .OrderBy(s => s.Ordem)
            .ToListAsync();

        short novaOrdem = 1;
        foreach (var serie in series)
        {
            serie.Ordem = novaOrdem++;
        }

        await _context.SaveChangesAsync();
    }
}