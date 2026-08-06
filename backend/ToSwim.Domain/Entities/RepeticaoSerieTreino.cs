using System;

namespace ToSwim.Domain.Entities;

public class RepeticaoSerieTreino
{
    public int CodRepeticaoSerieTreino { get; set; }
    public int CodSerieTreino { get; set; }
    public short NumeroRepeticao { get; set; }
    public int DistanciaRealM { get; set; }
    public decimal DuracaoSeg { get; set; } // int -> decimal
    public decimal? PaceSeg { get; set; }   // int? -> decimal?
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public SerieTreino? SerieTreino { get; set; }
}