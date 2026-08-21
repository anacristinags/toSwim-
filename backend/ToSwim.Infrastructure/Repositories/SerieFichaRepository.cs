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

    public async Task<short> ObterProximaOrdemAsync(int codFicha)
    {
        var maxOrdem = await _context.SeriesFicha
            .Where(s => s.CodFicha == codFicha)
            .MaxAsync(s => (short?)s.Ordem);

        return (short)((maxOrdem ?? 0) + 1);
    }

    public async Task DeslocarOrdensAPartirAsync(int codFicha, short ordemMinima)
    {
        var series = await _context.SeriesFicha
            .Where(s => s.CodFicha == codFicha && s.Ordem >= ordemMinima)
            .OrderByDescending(s => s.Ordem)
            .ToListAsync();

        if (series.Count == 0)
            return;

        // Duas fases para não violar o índice único (cod_ficha, ordem)
        foreach (var serie in series)
            serie.Ordem += 1000;
        await _context.SaveChangesAsync();

        foreach (var serie in series)
            serie.Ordem = (short)(serie.Ordem - 1000 + 1);
        await _context.SaveChangesAsync();
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

        if (series.Count == 0)
            return;

        short offset = 1000;
        foreach (var serie in series)
            serie.Ordem += offset;
        await _context.SaveChangesAsync();

        short novaOrdem = 1;
        foreach (var serie in series)
            serie.Ordem = novaOrdem++;

        await _context.SaveChangesAsync();
    }
}