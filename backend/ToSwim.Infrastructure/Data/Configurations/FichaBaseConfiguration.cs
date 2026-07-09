using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Data.Configurations;

public class FichaBaseConfiguration : IEntityTypeConfiguration<FichaBase>
{
    public void Configure(EntityTypeBuilder<FichaBase> builder)
    {
        builder.ToTable("ficha_base");

        builder.HasKey(f => f.CodFicha);
        builder.Property(f => f.CodFicha).HasColumnName("cod_ficha");
        builder.Property(f => f.CodUsuario).HasColumnName("cod_usuario").IsRequired();
        builder.Property(f => f.TituloFicha).HasColumnName("titulo_ficha").HasMaxLength(120).IsRequired();
        builder.Property(f => f.TipoFicha).HasColumnName("tipo_ficha").IsRequired();
        builder.Property(f => f.Status).HasColumnName("status").IsRequired();
        builder.Property(f => f.FichaCopiada).HasColumnName("ficha_copiada");
        builder.Property(f => f.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(f => f.UpdatedAt).HasColumnName("updated_at").IsRequired();

        // Relacionamentos
        builder.HasOne(f => f.Usuario)
            .WithMany()
            .HasForeignKey(f => f.CodUsuario)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(f => f.OrigemCopia)
            .WithMany()
            .HasForeignKey(f => f.FichaCopiada)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(f => f.CodUsuario).HasDatabaseName("idx_ficha_base_usuario");
    }
}