namespace ToSwim.Application.DTOs.ConfigPiscina;

public class ConfigPiscinaResponseDto
{
    public int CodPiscina { get; set; }
    public int CodUsuario { get; set; }
    public short TamanhoM { get; set; }
    public short FormaContagem { get; set; }
    public short Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}