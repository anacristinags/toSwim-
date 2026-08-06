using System;

namespace ToSwim.Application.DTOs.Repeticao;

public class RepeticaoResponseDto
{
    public int CodRepeticaoSerieTreino { get; set; }
    public int CodSerieTreino { get; set; }
    public short NumeroRepeticao { get; set; }
    public int DistanciaRealM { get; set; }
    public decimal DuracaoSeg { get; set; }
    public decimal? PaceSeg { get; set; }
    public DateTime CreatedAt { get; set; }
}