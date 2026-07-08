using System.ComponentModel.DataAnnotations;

namespace ToSwim.Application.DTOs.ConfigPiscina;

public class ConfigPiscinaRequestDto
{
    [Required(ErrorMessage = "Tamanho da piscina é obrigatório")]
    public short TamanhoM { get; set; }

    [Required(ErrorMessage = "Forma de contagem é obrigatória")]
    public short FormaContagem { get; set; }
}