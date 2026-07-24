using System;

namespace ToSwim.Application.DTOs.Repeticao;

public class RepeticaoResponseDto
{
    public int CodRepeticaoSerieTreino { get; set; }
    public int CodSerieTreino { get; set; }
    public short NumeroRepeticao { get; set; }
    public int DistanciaRealM { get; set; }
    public int DuracaoSeg { get; set; }
    public int? PaceSeg { get; set; }
    public DateTime CreatedAt { get; set; }
}