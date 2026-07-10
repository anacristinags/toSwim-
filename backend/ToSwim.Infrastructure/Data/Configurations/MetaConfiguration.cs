using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;

namespace ToSwim.Infrastructure.Data.Configurations;

public class MetaConfiguration : IEntityTypeConfiguration<Meta>
{
    public void Configure(EntityTypeBuilder<Meta> builder)
    {
        builder.ToTable("meta");

        builder.HasKey(m => m.CodMeta);

        builder.Property(m => m.CodMeta)
            .HasColumnName("cod_meta")
            .UseIdentityByDefaultColumn();

        builder.Property(m => m.CodUsuario)
            .HasColumnName("cod_usuario")
            .IsRequired();

        builder.Property(m => m.CodSerieFicha)
            .HasColumnName("cod_serie_ficha")
            .IsRequired();

        builder.Property(m => m.TituloMeta)
            .HasColumnName("titulo_meta")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(m => m.TipoNado)
            .HasColumnName("tipo_nado")
            .HasConversion<short>()
            .IsRequired();

        builder.Property(m => m.DistanciaAlvoM)
            .HasColumnName("distancia_alvo_m")
            .IsRequired();

        builder.Property(m => m.TempoAlvoSeg)
            .HasColumnName("tempo_alvo_seg")
            .IsRequired();

        builder.Property(m => m.PaceAlvoSeg)
            .HasColumnName("pace_alvo_seg")
            .IsRequired();

        builder.Property(m => m.ModoAvaliacao)
            .HasColumnName("modo_avaliacao")
            .HasConversion<short>()
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(m => m.Status)
            .HasColumnName("status")
            .HasConversion<short>()
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(m => m.DataInicio)
            .HasColumnName("data_inicio")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(m => m.DataFimPrevista)
            .HasColumnName("data_fim_prevista")
            .HasColumnType("timestamp with time zone");

        builder.Property(m => m.Observacoes)
            .HasColumnName("observacoes")
            .HasColumnType("text");

        builder.Property(m => m.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(m => m.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        // Relacionamentos (FKs)
        builder.HasOne(m => m.Usuario)
            .WithMany()
            .HasForeignKey(m => m.CodUsuario)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.SerieFicha)
            .WithMany()
            .HasForeignKey(m => m.CodSerieFicha)
            .OnDelete(DeleteBehavior.Cascade);

        // Índices no banco PostgreSQL
        builder.HasIndex(m => m.CodUsuario)
            .HasDatabaseName("idx_meta_usuario");

        builder.HasIndex(m => m.CodSerieFicha)
            .HasDatabaseName("idx_meta_serie_ficha");
    }
}