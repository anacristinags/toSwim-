using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Data.Configurations;

public class TreinoMetaConfiguration : IEntityTypeConfiguration<TreinoMeta>
{
    public void Configure(EntityTypeBuilder<TreinoMeta> builder)
    {
        builder.ToTable("treino_meta");

        builder.HasKey(tm => tm.CodTreinoMeta);

        builder.Property(tm => tm.CodTreinoMeta)
            .HasColumnName("cod_treino_meta")
            .UseIdentityByDefaultColumn();

        builder.Property(tm => tm.CodTreino)
            .HasColumnName("cod_treino")
            .IsRequired();

        builder.Property(tm => tm.CodMeta)
            .HasColumnName("cod_meta")
            .IsRequired();

        builder.Property(tm => tm.CodSerieTreino)
            .HasColumnName("cod_serie_treino");

        builder.Property(tm => tm.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        // Relacionamentos (FKs)
        builder.HasOne(tm => tm.Treino)
            .WithMany()
            .HasForeignKey(tm => tm.CodTreino)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tm => tm.Meta)
            .WithMany()
            .HasForeignKey(tm => tm.CodMeta)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(tm => tm.SerieTreino)
            .WithMany()
            .HasForeignKey(tm => tm.CodSerieTreino)
            .OnDelete(DeleteBehavior.SetNull);

        // Chave composta com série: UNIQUE (cod_treino, cod_meta, cod_serie_treino) WHERE cod_serie_treino IS NOT NULL
        builder.HasIndex(tm => new { tm.CodTreino, tm.CodMeta, tm.CodSerieTreino })
            .IsUnique()
            .HasFilter("cod_serie_treino IS NOT NULL")
            .HasDatabaseName("uq_treino_meta_serie");

        // Chave composta sem série: UNIQUE (cod_treino, cod_meta) WHERE cod_serie_treino IS NULL
        builder.HasIndex(tm => new { tm.CodTreino, tm.CodMeta })
            .IsUnique()
            .HasFilter("cod_serie_treino IS NULL")
            .HasDatabaseName("uq_treino_meta_geral");

        // Índices adicionais para consultas eficientes
        builder.HasIndex(tm => tm.CodTreino)
            .HasDatabaseName("idx_treino_meta_treino");

        builder.HasIndex(tm => tm.CodMeta)
            .HasDatabaseName("idx_treino_meta_meta");
    }
}