using ToSwim.Domain.Enums;

namespace ToSwim.Domain.Entities;

public class SerieFicha
{
    public int CodSerieFicha { get; set; }
    public int CodFicha { get; set; }
    public short Ordem { get; set; }
    public TipoNado TipoNado { get; set; }
    public int QuantidadeRepeticoes { get; set; }
    public int DistanciaM { get; set; }
    public int TempoPausaSeg { get; set; }
    public bool IsGoalSeries { get; set; }
    public string? Observacoes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Propriedades de Navegação
    public FichaBase? Ficha { get; set; }
}