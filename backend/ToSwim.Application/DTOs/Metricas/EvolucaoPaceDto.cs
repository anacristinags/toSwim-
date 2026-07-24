using System;
using ToSwim.Domain.Enums;

namespace ToSwim.Application.DTOs.Metricas;

public class EvolucaoPaceDto
{
    public DateTime Data { get; set; }
    public string TituloTreino { get; set; } = string.Empty;
    public TipoNado TipoNado { get; set; }
    public int DistanciaM { get; set; }
    public int PaceSeg { get; set; }
}