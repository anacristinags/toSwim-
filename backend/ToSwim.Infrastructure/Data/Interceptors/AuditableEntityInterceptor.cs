using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ToSwim.Domain.Entities;

namespace ToSwim.Infrastructure.Data.Interceptors;

/// <summary>
/// Interceptor de auditoria que garante que toda entidade <see cref="IAuditable"/> seja persistida
/// com valores <see cref="DateTime"/> em <see cref="DateTimeKind.Utc"/>.
/// </summary>
/// <remarks>
/// O Npgsql, com o comportamento legado desativado (<c>Npgsql.EnableLegacyTimestampBehavior = false</c>),
/// recusa gravar em colunas <c>timestamp with time zone</c> valores cujo <see cref="DateTime.Kind"/>
/// seja diferente de <see cref="DateTimeKind.Utc"/>. Lança:
/// <c>ArgumentException: Cannot write DateTime with Kind=Unspecified to PostgreSQL type
/// 'timestamp with time zone', only UTC is supported.</c>
///
/// Centralizando a atribuição das datas aqui, eliminamos a exceção de forma consistente em todo o
/// projeto, independentemente de o serviço/repositório ter ou não definido as datas manualmente.
/// O valor padrão (<see cref="DateTime.MinValue"/>, cujo Kind é Unspecified) nunca mais chega ao banco.
/// </remarks>
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAuditableEntities(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    /// <summary>
    /// Para entidades adicionadas (INSERT), define <see cref="IAuditable.CreatedAt"/> e
    /// <see cref="IAuditable.UpdatedAt"/> quando ainda não foram informadas (ou estão com valor padrão).
    /// Para entidades modificadas (UPDATE), atualiza <see cref="IAuditable.UpdatedAt"/>.
    /// </summary>
    private static void UpdateAuditableEntities(DbContext? context)
    {
        if (context is null)
            return;

        var utcNow = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<IAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.CreatedAt == default)
                        entry.Entity.CreatedAt = utcNow;

                    // Sempre define o UpdatedAt no INSERT; se a trigger do PostgreSQL regravar, não há conflito.
                    entry.Entity.UpdatedAt = utcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = utcNow;
                    // Garante que CreatedAt nunca seja alterado em updates mesmo se vier Unspecified do cliente.
                    if (entry.Entity.CreatedAt == default)
                        entry.Entity.CreatedAt = utcNow;
                    break;
            }
        }
    }
}
