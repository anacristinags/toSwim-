namespace ToSwim.Domain.Entities;

/// <summary>
/// Marca entidades que possuem as colunas de auditoria <c>created_at</c> e <c>updated_at</c>,
/// persistidas no PostgreSQL como <c>timestamp with time zone</c> (timestamptz).
/// </summary>
/// <remarks>
/// O Npgsql, com o comportamento legado desativado (Npgsql.EnableLegacyTimestampBehavior = false),
/// exige que valores gravados em colunas timestamptz tenham <see cref="DateTime.Kind"/> igual a
/// <see cref="DateTimeKind.Utc"/>. Por isso, os valores dessas propriedades devem sempre ser UTC.
/// </remarks>
public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
