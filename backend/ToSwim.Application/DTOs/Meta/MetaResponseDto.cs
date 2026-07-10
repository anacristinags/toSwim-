using System;
using ToSwim.Domain.Enums;

namespace ToSwim.Application.DTOs.Meta;

public class MetaResponseDto
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
    public StatusMeta Status { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFimPrevista { get; set; }
    public string? Observacoes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}