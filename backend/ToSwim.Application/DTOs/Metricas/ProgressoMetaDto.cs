using System.Collections.Generic;

namespace ToSwim.Application.DTOs.Metricas;

public class ProgressoMetaDto
{
    public int CodMeta { get; set; }
    public string TituloMeta { get; set; } = string.Empty;
    public int DistanciaAlvoM { get; set; }
    public int TempoAlvoSeg { get; set; }
    public int PaceAlvoSeg { get; set; }
    public int? MelhorTempoRealizadoSeg { get; set; }
    public int? MelhorPaceRealizadoSeg { get; set; }
    public double PercentualAtingimento { get; set; } // De 0.0 a 100.0
    public List<EvolucaoPaceDto> HistoricoTentativas { get; set; } = new();
}