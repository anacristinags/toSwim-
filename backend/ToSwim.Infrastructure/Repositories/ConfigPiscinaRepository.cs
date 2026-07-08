using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Infrastructure.Repositories;

public class ConfigPiscinaRepository : IConfigPiscinaRepository
{
    private readonly ToSwimDbContext _context;

    public ConfigPiscinaRepository(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<ConfigPiscina?> BuscarPorUsuarioAsync(int codUsuario)
        => await _context.ConfigPiscinas
            .FirstOrDefaultAsync(p => p.CodUsuario == codUsuario);

    public async Task<ConfigPiscina> CriarAsync(ConfigPiscina configPiscina)
    {
        _context.ConfigPiscinas.Add(configPiscina);
        await _context.SaveChangesAsync();
        return configPiscina;
    }

    public async Task<ConfigPiscina> AtualizarAsync(ConfigPiscina configPiscina)
    {
        _context.ConfigPiscinas.Update(configPiscina);
        await _context.SaveChangesAsync();
        return configPiscina;
    }

    public async Task<bool> ExistePorUsuarioAsync(int codUsuario)
        => await _context.ConfigPiscinas
            .AnyAsync(p => p.CodUsuario == codUsuario);
}