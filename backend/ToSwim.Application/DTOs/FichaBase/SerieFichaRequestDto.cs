using System.ComponentModel.DataAnnotations;

namespace ToSwim.Application.DTOs.FichaBase;

public class SerieFichaRequestDto
{
    [Range(1, 100, ErrorMessage = "A ordem deve ser maior que 0")]
    public short? Ordem { get; set; }

    [Required(ErrorMessage = "O tipo de nado e obrigatorio")]
    [Range(0, 3, ErrorMessage = "Tipo de nado invalido (0=Crawl, 1=Costas, 2=Borboleta, 3=Peito)")]
    public short TipoNado { get; set; }

    [Required(ErrorMessage = "A quantidade de repeticoes e obrigatoria")]
    [Range(1, 1000, ErrorMessage = "A quantidade deve ser maior que 0")]
    public int QuantidadeRepeticoes { get; set; }

    [Required(ErrorMessage = "A distancia e obrigatoria")]
    [Range(1, 10000, ErrorMessage = "A distancia deve ser maior que 0 metros")]
    public int DistanciaM { get; set; }

    [Required(ErrorMessage = "O tempo de pausa e obrigatorio")]
    [Range(0, 3600, ErrorMessage = "O tempo de pausa nao pode ser negativo")]
    public decimal TempoPausaSeg { get; set; }

    public bool IsGoalSeries { get; set; } = false;

    public string? Observacoes { get; set; }
}
