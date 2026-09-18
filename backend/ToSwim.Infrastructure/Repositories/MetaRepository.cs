using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Infrastructure.Repositories;

public class MetaRepository : IMetaRepository
{
    private readonly ToSwimDbContext _context;

    public MetaRepository(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<Meta?> ObterPorIdAsync(int codMeta, int codUsuario)
    {
        return await _context.Metas
            .Include(m => m.SerieFicha!)
                .ThenInclude(sf => sf.Ficha)
            .FirstOrDefaultAsync(m => m.CodMeta == codMeta && m.CodUsuario == codUsuario);
    }

    public async Task<IEnumerable<Meta>> ObterTodasPorUsuarioAsync(int codUsuario, StatusMeta? status)
    {
        var query = _context.Metas
            .Include(m => m.SerieFicha!)
                .ThenInclude(sf => sf.Ficha)
            .AsNoTracking()
            .Where(m => m.CodUsuario == codUsuario);

        if (status.HasValue)
        {
            query = query.Where(m => m.Status == status.Value);
        }

        return await query.OrderByDescending(m => m.CreatedAt).ToListAsync();
    }

    public async Task AdicionarAsync(Meta meta)
    {
        await _context.Metas.AddAsync(meta);
    }

    public void Atualizar(Meta meta)
    {
        _context.Metas.Update(meta);
    }

    public void Remover(Meta meta)
    {
        _context.Metas.Remove(meta);
    }

    public async Task<bool> ExisteMetaAtivaParaSerieAsync(int codUsuario, int codSerieFicha)
    {
        return await _context.Metas
            .AnyAsync(m => m.CodUsuario == codUsuario
                           && m.CodSerieFicha == codSerieFicha
                           && m.Status == StatusMeta.Ativa);
    }

    public async Task<bool> SalvarAlteracoesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}