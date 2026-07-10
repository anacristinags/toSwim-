using ToSwim.Domain.Enums;

namespace ToSwim.Domain.Entities;

public class ConfigPiscina : IAuditable
{
    public int CodPiscina { get; set; }
    public int CodUsuario { get; set; }
    public short TamanhoM { get; set; }
    public FormaContagem FormaContagem { get; set; }
    public short Status { get; set; } = 1;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Usuario? Usuario { get; set; }
}