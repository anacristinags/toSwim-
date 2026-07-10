using System;
using ToSwim.Domain.Enums;

namespace ToSwim.Domain.Entities;

public class Meta : IAuditable
{
    public int CodMeta { get; set; }
    public int CodUsuario { get; set; }
    public int CodSerieFicha { get; set; }
    public string TituloMeta { get; set; } = string.Empty;
    public TipoNado TipoNado { get; set; }
    public int DistanciaAlvoM { get; set; }
    public int TempoAlvoSeg { get; set; }
    public int PaceAlvoSeg { get; set; }
    public ModoAvaliacaoMeta ModoAvaliacao { get; set; }
    public StatusMeta Status { get; set; } = StatusMeta.Ativa;
    public DateTime DataInicio { get; set; } = DateTime.UtcNow;
    public DateTime? DataFimPrevista { get; set; }
    public string? Observacoes { get; set; }

    // Propriedades da interface IAuditable (gerenciadas pelo Interceptor)
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Propriedades de Navegação Relacional
    public Usuario? Usuario { get; set; }
    public SerieFicha? SerieFicha { get; set; }
}