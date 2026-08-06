using System;
using ToSwim.Domain.Enums;

namespace ToSwim.Domain.Entities;

public class SerieTreino : IAuditable
{
    public int CodSerieTreino { get; set; }
    public int CodTreino { get; set; }
    public int? CodSerieFicha { get; set; }
    public int? CodMeta { get; set; }
    public short Ordem { get; set; }
    public TipoNado TipoNado { get; set; }
    public int QuantidadeRepeticoesPlanejada { get; set; }
    public int DistanciaPlanejadaM { get; set; }
    public decimal TempoPausaSeg { get; set; } // int -> decimal
    public decimal? TempoTotalSeg { get; set; } // int? -> decimal?
    public int? DistanciaTotalM { get; set; }
    public decimal? PaceMedioSeg { get; set; }  // int? -> decimal?
    public string? Observacoes { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Treino? Treino { get; set; }
    public SerieFicha? SerieFicha { get; set; }
    public Meta? Meta { get; set; }
}