using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuario");

        builder.HasKey(u => u.CodUsuario);
        builder.Property(u => u.CodUsuario).HasColumnName("cod_usuario");
        builder.Property(u => u.Nome).HasColumnName("nome").HasMaxLength(100).IsRequired();
        builder.Property(u => u.Email).HasColumnName("email").HasMaxLength(150).IsRequired();
        builder.Property(u => u.SenhaHash).HasColumnName("senha_hash").HasMaxLength(255).IsRequired();
        builder.Property(u => u.StatusConta).HasColumnName("status_conta").IsRequired();
        builder.Property(u => u.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(u => u.UpdatedAt).HasColumnName("updated_at").IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();
    }
}