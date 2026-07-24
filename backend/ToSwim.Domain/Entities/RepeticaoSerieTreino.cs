using System;

namespace ToSwim.Domain.Entities;

public class RepeticaoSerieTreino
{
    public int CodRepeticaoSerieTreino { get; set; }
    public int CodSerieTreino { get; set; }
    public short NumeroRepeticao { get; set; }
    public int DistanciaRealM { get; set; }
    public int DuracaoSeg { get; set; }
    public int? PaceSeg { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Gerado no construtor

    // Propriedades de Navegação Relacional
    public SerieTreino? SerieTreino { get; set; }
}