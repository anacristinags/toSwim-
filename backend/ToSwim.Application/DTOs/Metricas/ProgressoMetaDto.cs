using System.Collections.Generic;

namespace ToSwim.Application.DTOs.Metricas;

public class ProgressoMetaDto
{
    public int CodMeta { get; set; }
    public string TituloMeta { get; set; } = string.Empty;
    public int DistanciaAlvoM { get; set; }
    public decimal TempoAlvoSeg { get; set; }
    public decimal PaceAlvoSeg { get; set; }
    public decimal? MelhorTempoRealizadoSeg { get; set; }
    public decimal? MelhorPaceRealizadoSeg { get; set; }
    public double PercentualAtingimento { get; set; } // De 0.0 a 100.0
    public List<EvolucaoPaceDto> HistoricoTentativas { get; set; } = new();
}