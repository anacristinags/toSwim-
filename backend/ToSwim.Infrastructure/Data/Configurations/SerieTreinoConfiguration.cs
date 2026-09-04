using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Data.Configurations;

public class SerieTreinoConfiguration : IEntityTypeConfiguration<SerieTreino>
{
    public void Configure(EntityTypeBuilder<SerieTreino> builder)
    {
        builder.ToTable("serie_treino");

        builder.HasKey(s => s.CodSerieTreino);

        builder.Property(s => s.CodSerieTreino)
            .HasColumnName("cod_serie_treino")
            .UseIdentityByDefaultColumn();

        builder.Property(s => s.CodTreino)
            .HasColumnName("cod_treino")
            .IsRequired();

        builder.Property(s => s.CodSerieFicha)
            .HasColumnName("cod_serie_ficha");

        builder.Property(s => s.CodMeta)
            .HasColumnName("cod_meta");

        builder.Property(s => s.Ordem)
            .HasColumnName("ordem")
            .IsRequired();

        builder.Property(s => s.TipoNado)
            .HasColumnName("tipo_nado")
            .HasConversion<short>()
            .IsRequired();

        builder.Property(s => s.QuantidadeRepeticoesPlanejada)
            .HasColumnName("quantidade_repeticoes_planejada")
            .IsRequired();

        builder.Property(s => s.DistanciaPlanejadaM)
            .HasColumnName("distancia_planejada_m")
            .IsRequired();

        builder.Property(s => s.TempoPausaSeg)
            .HasColumnName("tempo_pausa_seg")
            .HasColumnType("numeric(10,2)")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(s => s.TempoTotalSeg)
            .HasColumnName("tempo_total_seg")
            .HasColumnType("numeric(10,2)");

        builder.Property(s => s.DistanciaTotalM)
            .HasColumnName("distancia_total_m");

        builder.Property(s => s.PaceMedioSeg)
            .HasColumnName("pace_medio_seg")
            .HasColumnType("numeric(10,2)");

        builder.Property(s => s.IgnorarNoPace)
            .HasColumnName("ignorar_no_pace")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(s => s.Observacoes)
            .HasColumnName("observacoes")
            .HasColumnType("text");

        builder.Property(s => s.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(s => s.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        // Relacionamentos (FKs)
        builder.HasOne(s => s.Treino)
            .WithMany(t => t.SeriesTreino)
            .HasForeignKey(s => s.CodTreino)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.SerieFicha)
            .WithMany()
            .HasForeignKey(s => s.CodSerieFicha)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(s => s.Meta)
            .WithMany()
            .HasForeignKey(s => s.CodMeta)
            .OnDelete(DeleteBehavior.SetNull);

        // Chave �nica Composta: Ordem dentro do mesmo Treino
        builder.HasIndex(s => new { s.CodTreino, s.Ordem })
            .IsUnique()
            .HasDatabaseName("uq_serie_treino_ordem");

        // �ndices criados na Migration 008
        builder.HasIndex(s => s.CodTreino)
            .HasDatabaseName("idx_serie_treino_treino");

        builder.HasIndex(s => s.CodMeta)
            .HasDatabaseName("idx_serie_treino_meta");
    }
}