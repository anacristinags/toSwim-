using System.ComponentModel.DataAnnotations;

namespace ToSwim.Application.DTOs.FichaBase;

public class SerieFichaRequestDto
{
    [Required(ErrorMessage = "A ordem da série é obrigatória")]
    [Range(1, 100, ErrorMessage = "A ordem deve ser maior que 0")]
    public short Ordem { get; set; }

    [Required(ErrorMessage = "O tipo de nado é obrigatório")]
    [Range(0, 3, ErrorMessage = "Tipo de nado inválido (0=Crawl, 1=Costas, 2=Borboleta, 3=Peito)")]
    public short TipoNado { get; set; }

    [Required(ErrorMessage = "A quantidade de repetições é obrigatória")]
    [Range(1, 1000, ErrorMessage = "A quantidade deve ser maior que 0")]
    public int QuantidadeRepeticoes { get; set; }

    [Required(ErrorMessage = "A distância é obrigatória")]
    [Range(1, 10000, ErrorMessage = "A distância deve ser maior que 0 metros")]
    public int DistanciaM { get; set; }

    [Required(ErrorMessage = "O tempo de pausa é obrigatório")]
    [Range(0, 3600, ErrorMessage = "O tempo de pausa não pode ser negativo")]
    public int TempoPausaSeg { get; set; }

    public bool IsGoalSeries { get; set; } = false;

    public string? Observacoes { get; set; }
}