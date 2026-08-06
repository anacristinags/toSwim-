using System;
using ToSwim.Domain.Enums;

namespace ToSwim.Domain.Entities;

public class SerieFicha : IAuditable
{
    public int CodSerieFicha { get; set; }
    public int CodFicha { get; set; }
    public short Ordem { get; set; }
    public TipoNado TipoNado { get; set; }
    public int QuantidadeRepeticoes { get; set; }
    public int DistanciaM { get; set; }
    public decimal TempoPausaSeg { get; set; } // int -> decimal
    public bool IsGoalSeries { get; set; }
    public string? Observacoes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public FichaBase? Ficha { get; set; }
}