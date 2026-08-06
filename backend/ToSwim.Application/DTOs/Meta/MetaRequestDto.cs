using System;
using ToSwim.Domain.Enums;

namespace ToSwim.Application.DTOs.Meta;

public class MetaRequestDto
{
    public int CodSerieFicha { get; set; }
    public string TituloMeta { get; set; } = string.Empty;
    public TipoNado TipoNado { get; set; }
    public int DistanciaAlvoM { get; set; }
    public decimal TempoAlvoSeg { get; set; }
    public ModoAvaliacaoMeta ModoAvaliacao { get; set; }
    public DateTime DataInicio { get; set; } = DateTime.UtcNow;
    public DateTime? DataFimPrevista { get; set; }
    public string? Observacoes { get; set; }
}