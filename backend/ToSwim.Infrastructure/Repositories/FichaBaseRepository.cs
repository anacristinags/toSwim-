using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Infrastructure.Repositories;

public class FichaBaseRepository : IFichaBaseRepository
{
    private readonly ToSwimDbContext _context;

    public FichaBaseRepository(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FichaBase>> ListarPorUsuarioAsync(int codUsuario)
    {
        return await _context.FichasBase
            .AsNoTracking()
            .Include(f => f.Series.OrderBy(s => s.Ordem))
            .Where(f => f.CodUsuario == codUsuario && f.Status == 1)
            .OrderByDescending(f => f.CreatedAt)
            .ToListAsync();
    }

    public async Task<FichaBase?> BuscarPorIdAsync(int codFicha, int codUsuario)
    {
        return await _context.FichasBase
            .Include(f => f.Series.OrderBy(s => s.Ordem))
            .FirstOrDefaultAsync(f => f.CodFicha == codFicha && f.CodUsuario == codUsuario);
    }

    public async Task<int> ContarFichasAtivasAsync(int codUsuario)
    {
        return await _context.FichasBase
            .CountAsync(f => f.CodUsuario == codUsuario && f.Status == 1);
    }

    public async Task<FichaBase> CriarAsync(FichaBase ficha)
    {
        _context.FichasBase.Add(ficha);
        await _context.SaveChangesAsync();
        return ficha;
    }

    public async Task<FichaBase> AtualizarAsync(FichaBase ficha)
    {
        _context.FichasBase.Update(ficha);
        await _context.SaveChangesAsync();
        return ficha;
    }

    public async Task DeletarLogicamenteAsync(FichaBase ficha)
    {
        ficha.Status = 0; 
        _context.FichasBase.Update(ficha);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> TemVinculosQueImpedemExclusaoAsync(int codFicha)
    {
        var conn = _context.Database.GetDbConnection();
        using var cmd = conn.CreateCommand();

        cmd.CommandText = @"
            SELECT 
                (SELECT COUNT(1) FROM treino WHERE cod_ficha = @codFicha) +
                (SELECT COUNT(1) FROM meta m 
                 INNER JOIN serie_ficha sf ON m.cod_serie_ficha = sf.cod_serie_ficha 
                 WHERE sf.cod_ficha = @codFicha) AS total_vinculos";

        var param = cmd.CreateParameter();
        param.ParameterName = "@codFicha";
        param.Value = codFicha;
        cmd.Parameters.Add(param);

        if (conn.State != System.Data.ConnectionState.Open)
            await conn.OpenAsync();

        var count = Convert.ToInt64(await cmd.ExecuteScalarAsync() ?? 0);
        return count > 0;
    }

    public async Task ExcluirFisicamenteAsync(FichaBase ficha)
    {
        _context.FichasBase.Remove(ficha);
        await _context.SaveChangesAsync();
    }
}