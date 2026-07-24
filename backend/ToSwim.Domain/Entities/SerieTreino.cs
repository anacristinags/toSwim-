using System;
using ToSwim.Domain.Enums;

namespace ToSwim.Domain.Entities;

public class SerieTreino : IAuditable
{
    public int CodSerieTreino { get; set; }
    public int CodTreino { get; set; }
    public int? CodSerieFicha { get; set; } // ON DELETE SET NULL
    public int? CodMeta { get; set; }       // ON DELETE SET NULL
    public short Ordem { get; set; }
    public TipoNado TipoNado { get; set; }
    public int QuantidadeRepeticoesPlanejada { get; set; }
    public int DistanciaPlanejadaM { get; set; }
    public int TempoPausaSeg { get; set; }
    public int? TempoTotalSeg { get; set; }
    public int? DistanciaTotalM { get; set; }
    public int? PaceMedioSeg { get; set; }
    public string? Observacoes { get; set; }

    // Propriedades de Auditoria (IAuditable)
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Propriedades de Navegação Relacional
    public Treino? Treino { get; set; }
    public SerieFicha? SerieFicha { get; set; }
    public Meta? Meta { get; set; }
}