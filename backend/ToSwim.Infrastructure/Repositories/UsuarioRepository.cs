using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Data;
using ToSwim.Infrastructure.Repositories.Interfaces;

namespace ToSwim.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ToSwimDbContext _context;

    public UsuarioRepository(ToSwimDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> BuscarPorEmailAsync(string email)
        => await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == email.ToLower());

    public async Task<Usuario?> BuscarPorIdAsync(int id)
        => await _context.Usuarios
            .FirstOrDefaultAsync(u => u.CodUsuario == id);

    public async Task<bool> EmailExisteAsync(string email)
        => await _context.Usuarios
            .AnyAsync(u => u.Email == email.ToLower());

    public async Task<Usuario> CriarAsync(Usuario usuario)
    {
        usuario.Email = usuario.Email.ToLower();
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario> AtualizarAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }
}