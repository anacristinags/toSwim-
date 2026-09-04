using System;
using ToSwim.Domain.Enums;

namespace ToSwim.Application.DTOs.SerieTreino;

public class SerieTreinoResponseDto
{
    public int CodSerieTreino { get; set; }
    public int CodTreino { get; set; }
    public int? CodSerieFicha { get; set; }
    public int? CodMeta { get; set; }
    public short Ordem { get; set; }
    public TipoNado TipoNado { get; set; }
    public int QuantidadeRepeticoesPlanejada { get; set; }
    public int DistanciaPlanejadaM { get; set; }
    public decimal TempoPausaSeg { get; set; }
    public decimal? TempoTotalSeg { get; set; }
    public int? DistanciaTotalM { get; set; }
    public decimal? PaceMedioSeg { get; set; }
    public bool IgnorarNoPace { get; set; }
    public string? Observacoes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}