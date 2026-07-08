using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Data.Configurations;

public class ConfigPiscinaConfiguration : IEntityTypeConfiguration<ConfigPiscina>
{
    public void Configure(EntityTypeBuilder<ConfigPiscina> builder)
    {
        builder.ToTable("config_piscina");

        builder.HasKey(p => p.CodPiscina);
        builder.Property(p => p.CodPiscina).HasColumnName("cod_piscina");
        builder.Property(p => p.CodUsuario).HasColumnName("cod_usuario").IsRequired();
        builder.Property(p => p.TamanhoM).HasColumnName("tamanho_m").IsRequired();
        builder.Property(p => p.FormaContagem).HasColumnName("forma_contagem").IsRequired();
        builder.Property(p => p.Status).HasColumnName("status").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasOne(p => p.Usuario)
            .WithMany()
            .HasForeignKey(p => p.CodUsuario);

        builder.HasIndex(p => p.CodUsuario).IsUnique();
    }
}