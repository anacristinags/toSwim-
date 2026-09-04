using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Data.Configurations;

public class SerieFichaConfiguration : IEntityTypeConfiguration<SerieFicha>
{
    public void Configure(EntityTypeBuilder<SerieFicha> builder)
    {
        builder.ToTable("serie_ficha");

        builder.HasKey(s => s.CodSerieFicha);
        builder.Property(s => s.CodSerieFicha).HasColumnName("cod_serie_ficha");
        builder.Property(s => s.CodFicha).HasColumnName("cod_ficha").IsRequired();
        builder.Property(s => s.Ordem).HasColumnName("ordem").IsRequired();
        builder.Property(s => s.TipoNado).HasColumnName("tipo_nado").IsRequired();
        builder.Property(s => s.QuantidadeRepeticoes).HasColumnName("quantidade_repeticoes").IsRequired();
        builder.Property(s => s.DistanciaM).HasColumnName("distancia_m").IsRequired();
        builder.Property(s => s.TempoPausaSeg).HasColumnName("tempo_pausa_seg").HasColumnType("numeric(10,2)").IsRequired();
        builder.Property(s => s.IsGoalSeries).HasColumnName("is_goal_series").IsRequired();
        builder.Property(s => s.IgnorarNoPace).HasColumnName("ignorar_no_pace").HasDefaultValue(false).IsRequired();
        builder.Property(s => s.Observacoes).HasColumnName("observacoes");
        builder.Property(s => s.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(s => s.UpdatedAt).HasColumnName("updated_at").IsRequired();

        // Relacionamentos
        builder.HasOne(s => s.Ficha)
            .WithMany(f => f.Series)
            .HasForeignKey(s => s.CodFicha)
            .OnDelete(DeleteBehavior.Cascade);

        // Unicidade de Ordem por Ficha
        builder.HasIndex(s => new { s.CodFicha, s.Ordem })
            .IsUnique()
            .HasDatabaseName("uq_serie_ficha_ordem");

        builder.HasIndex(s => s.CodFicha).HasDatabaseName("idx_serie_ficha_ficha");
    }
}