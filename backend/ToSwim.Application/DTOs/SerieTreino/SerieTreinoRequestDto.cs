using ToSwim.Domain.Enums;

namespace ToSwim.Application.DTOs.SerieTreino;

public class SerieTreinoRequestDto
{
    public int? CodSerieFicha { get; set; }
    public int? CodMeta { get; set; }
    public short Ordem { get; set; }
    public TipoNado TipoNado { get; set; }
    public int QuantidadeRepeticoesPlanejada { get; set; }
    public int DistanciaPlanejadaM { get; set; }
    public decimal TempoPausaSeg { get; set; }
    public decimal? TempoTotalSeg { get; set; }
    public int? DistanciaTotalM { get; set; }
    public string? Observacoes { get; set; }
}