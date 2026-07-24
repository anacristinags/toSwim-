using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Infrastructure.Repositories;

public class RepeticaoSerieTreinoRepository : IRepeticaoSerieTreinoRepository
{
    private readonly ToSwimDbContext _context;

    public RepeticaoSerieTreinoRepository(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<RepeticaoSerieTreino?> ObterPorIdAsync(int codRepeticao, int codUsuario)
    {
        return await _context.RepeticoesSerieTreino
            .Include(r => r.SerieTreino)
                .ThenInclude(s => s!.Treino)
            .FirstOrDefaultAsync(r => r.CodRepeticaoSerieTreino == codRepeticao
                                      && r.SerieTreino!.Treino!.CodUsuario == codUsuario);
    }

    public async Task<IEnumerable<RepeticaoSerieTreino>> ObterPorSerieTreinoAsync(int codSerieTreino, int codUsuario)
    {
        return await _context.RepeticoesSerieTreino
            .AsNoTracking()
            .Include(r => r.SerieTreino)
                .ThenInclude(s => s!.Treino)
            .Where(r => r.CodSerieTreino == codSerieTreino
                        && r.SerieTreino!.Treino!.CodUsuario == codUsuario)
            .OrderBy(r => r.NumeroRepeticao)
            .ToListAsync();
    }

    public async Task AdicionarAsync(RepeticaoSerieTreino repeticao)
    {
        await _context.RepeticoesSerieTreino.AddAsync(repeticao);
    }

    public void Atualizar(RepeticaoSerieTreino repeticao)
    {
        _context.RepeticoesSerieTreino.Update(repeticao);
    }

    public void Remover(RepeticaoSerieTreino repeticao)
    {
        _context.RepeticoesSerieTreino.Remove(repeticao);
    }

    public async Task<bool> ExisteNumeroRepeticaoNaSerieAsync(int codSerieTreino, short numeroRepeticao)
    {
        return await _context.RepeticoesSerieTreino
            .AnyAsync(r => r.CodSerieTreino == codSerieTreino && r.NumeroRepeticao == numeroRepeticao);
    }

    public async Task<bool> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}