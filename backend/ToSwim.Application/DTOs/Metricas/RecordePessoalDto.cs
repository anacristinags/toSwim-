using System;
using ToSwim.Domain.Enums;

namespace ToSwim.Application.DTOs.Metricas;

public class RecordePessoalDto
{
    public TipoNado TipoNado { get; set; }
    public int DistanciaM { get; set; }
    public int TempoRecordeSeg { get; set; }
    public int PaceRecordeSeg { get; set; }
    public DateTime DataConquista { get; set; }
    public string TituloTreino { get; set; } = string.Empty;
}