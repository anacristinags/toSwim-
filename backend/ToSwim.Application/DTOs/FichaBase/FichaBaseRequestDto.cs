using System.ComponentModel.DataAnnotations;

namespace ToSwim.Application.DTOs.FichaBase;

public class FichaBaseRequestDto
{
    [Required(ErrorMessage = "O t�tulo da ficha � obrigat�rio")]
    [MaxLength(120, ErrorMessage = "O t�tulo deve ter no m�ximo 120 caracteres")]
    public string TituloFicha { get; set; } = string.Empty;

    [Range(0, 1, ErrorMessage = "Tipo de ficha inv�lido (0=Comum, 1=Meta de Tempo)")]
    public short TipoFicha { get; set; } = 0;

    public short TamanhoPiscinaM { get; set; } = 25; // deve ser 25 ou 50
}