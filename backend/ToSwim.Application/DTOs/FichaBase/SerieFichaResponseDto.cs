namespace ToSwim.Application.DTOs.FichaBase;

public class SerieFichaResponseDto
{
    public int CodSerieFicha { get; set; }
    public int CodFicha { get; set; }
    public short Ordem { get; set; }
    public short TipoNado { get; set; }
    public int QuantidadeRepeticoes { get; set; }
    public int DistanciaM { get; set; }
    public int TempoPausaSeg { get; set; }
    public bool IsGoalSeries { get; set; }
    public string? Observacoes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}