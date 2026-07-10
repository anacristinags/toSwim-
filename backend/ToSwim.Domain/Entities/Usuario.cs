using ToSwim.Domain.Enums;

namespace ToSwim.Domain.Entities;

public class Usuario : IAuditable
{
    public int CodUsuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public StatusConta StatusConta { get; set; } = StatusConta.Ativo;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}