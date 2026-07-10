using Microsoft.EntityFrameworkCore;
using ToSwim.Domain.Entities;
using ToSwim.Infrastructure.Data.Configurations;

namespace ToSwim.Infrastructure.Data;

public class ToSwimDbContext : DbContext
{
    public ToSwimDbContext(DbContextOptions<ToSwimDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<ConfigPiscina> ConfigPiscinas => Set<ConfigPiscina>();
    public DbSet<FichaBase> FichasBase => Set<FichaBase>();
    public DbSet<SerieFicha> SeriesFicha => Set<SerieFicha>();
    public DbSet<Meta> Metas => Set<Meta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
        modelBuilder.ApplyConfiguration(new ConfigPiscinaConfiguration());
        modelBuilder.ApplyConfiguration(new FichaBaseConfiguration());
        modelBuilder.ApplyConfiguration(new SerieFichaConfiguration());
    }
}