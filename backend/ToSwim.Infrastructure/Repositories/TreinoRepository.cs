using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Infrastructure.Repositories;

public class TreinoRepository : ITreinoRepository
{
    private readonly ToSwimDbContext _context;

    public TreinoRepository(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<Treino?> ObterPorIdAsync(int codTreino, int codUsuario)
    {
        return await _context.Treinos
            .Include(t => t.SeriesTreino.OrderBy(s => s.Ordem))
            .FirstOrDefaultAsync(t => t.CodTreino == codTreino && t.CodUsuario == codUsuario);
    }

    public async Task<IEnumerable<Treino>> ObterTodosPorUsuarioAsync(int codUsuario, StatusTreino? status, int pagina, int tamanhoPagina)
    {
        var query = _context.Treinos
            .AsNoTracking()
            .Where(t => t.CodUsuario == codUsuario);

        if (status.HasValue)
        {
            query = query.Where(t => t.Status == status.Value);
        }

        return await query
            .OrderByDescending(t => t.DataTreino)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Treino treino)
    {
        await _context.Treinos.AddAsync(treino);
    }

    public void Atualizar(Treino treino)
    {
        _context.Treinos.Update(treino);
    }

    public async Task<bool> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}