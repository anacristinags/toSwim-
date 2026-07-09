using System.ComponentModel.DataAnnotations;

namespace ToSwim.Application.DTOs.FichaBase;

public class FichaBaseRequestDto
{
    [Required(ErrorMessage = "O título da ficha é obrigatório")]
    [MaxLength(120, ErrorMessage = "O título deve ter no máximo 120 caracteres")]
    public string TituloFicha { get; set; } = string.Empty;

    [Range(0, 1, ErrorMessage = "Tipo de ficha inválido (0=Comum, 1=Meta de Tempo)")]
    public short TipoFicha { get; set; } = 0;
}