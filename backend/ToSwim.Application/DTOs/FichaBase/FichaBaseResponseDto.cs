namespace ToSwim.Application.DTOs.FichaBase;

public class FichaBaseResponseDto
{
    public int CodFicha { get; set; }
    public int CodUsuario { get; set; }
    public string TituloFicha { get; set; } = string.Empty;
    public short TipoFicha { get; set; }
    public short Status { get; set; }
    public int? FichaCopiada { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<SerieFichaResponseDto> Series { get; set; } = new();
}