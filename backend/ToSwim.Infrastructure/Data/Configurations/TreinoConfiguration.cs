using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToSwim.Domain.Entities;
using ToSwim.Domain.Enums;

namespace ToSwim.Infrastructure.Data.Configurations;

public class TreinoConfiguration : IEntityTypeConfiguration<Treino>
{
    public void Configure(EntityTypeBuilder<Treino> builder)
    {
        builder.ToTable("treino");

        builder.HasKey(t => t.CodTreino);

        builder.Property(t => t.CodTreino)
            .HasColumnName("cod_treino")
            .UseIdentityByDefaultColumn();

        builder.Property(t => t.CodUsuario)
            .HasColumnName("cod_usuario")
            .IsRequired();

        builder.Property(t => t.CodFicha)
            .HasColumnName("cod_ficha")
            .IsRequired();

        builder.Property(t => t.TituloTreino)
            .HasColumnName("titulo_treino")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(t => t.Observacao)
            .HasColumnName("observacao")
            .HasColumnType("text");

        builder.Property(t => t.DistanciaTotalM)
            .HasColumnName("distancia_total_m")
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(t => t.DuracaoTotalSeg)
            .HasColumnName("duracao_total_seg")
            .HasColumnType("numeric(10,2)")
            .HasDefaultValue(0).IsRequired();

        builder.Property(t => t.PaceMedioSeg)
            .HasColumnName("pace_medio_seg")
            .HasColumnType("numeric(10,2)");

        builder.Property(t => t.TamanhoPiscinaM)
            .HasColumnName("tamanho_piscina_m")
            .IsRequired();

        builder.Property(t => t.DataTreino)
            .HasColumnName("data_treino")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(t => t.Status)
            .HasColumnName("status")
            .HasConversion<short>()
            .HasDefaultValue(StatusTreino.Andamento)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp with time zone")
            .IsRequired();

        // Relacionamentos (FKs)
        builder.HasOne(t => t.Usuario)
            .WithMany()
            .HasForeignKey(t => t.CodUsuario)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(t => t.FichaBase)
            .WithMany()
            .HasForeignKey(t => t.CodFicha)
            .OnDelete(DeleteBehavior.Restrict); // Histórico protegido

        // Índices criados na Migration 007
        builder.HasIndex(t => t.CodUsuario)
            .HasDatabaseName("idx_treino_usuario");

        builder.HasIndex(t => t.CodFicha)
            .HasDatabaseName("idx_treino_ficha");

        builder.HasIndex(t => t.DataTreino)
            .HasDatabaseName("idx_treino_data");
    }
}