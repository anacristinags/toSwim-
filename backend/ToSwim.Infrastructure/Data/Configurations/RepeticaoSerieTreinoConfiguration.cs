using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Data.Configurations;

public class RepeticaoSerieTreinoConfiguration : IEntityTypeConfiguration<RepeticaoSerieTreino>
{
    public void Configure(EntityTypeBuilder<RepeticaoSerieTreino> builder)
    {
        builder.ToTable("repeticao_serie_treino");

        builder.HasKey(r => r.CodRepeticaoSerieTreino);

        builder.Property(r => r.CodRepeticaoSerieTreino)
            .HasColumnName("cod_repeticao_serie_treino")
            .UseIdentityByDefaultColumn();

        builder.Property(r => r.CodSerieTreino)
            .HasColumnName("cod_serie_treino")
            .IsRequired();

        builder.Property(r => r.NumeroRepeticao)
            .HasColumnName("numero_repeticao")
            .IsRequired();

        builder.Property(r => r.DistanciaRealM)
            .HasColumnName("distancia_real_m")
            .IsRequired();

        builder.Property(r => r.DuracaoSeg)
            .HasColumnName("duracao_seg")
            .HasColumnType("numeric(10,2)")
            .IsRequired();

        builder.Property(r => r.PaceSeg)
            .HasColumnName("pace_seg")
            .HasColumnType("numeric(10,2)");

        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        // Relacionamentos (FK)
        builder.HasOne(r => r.SerieTreino)
            .WithMany()
            .HasForeignKey(r => r.CodSerieTreino)
            .OnDelete(DeleteBehavior.Cascade);

        // Chave Única Composta: (cod_serie_treino, numero_repeticao)
        builder.HasIndex(r => new { r.CodSerieTreino, r.NumeroRepeticao })
            .IsUnique()
            .HasDatabaseName("uq_repeticao_serie_numero");

        // Índice para busca frequente
        builder.HasIndex(r => r.CodSerieTreino)
            .HasDatabaseName("idx_repeticao_serie");
    }
}