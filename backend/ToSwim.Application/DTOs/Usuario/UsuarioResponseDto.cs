namespace ToSwim.Application.DTOs.Usuario;

public class UsuarioResponseDto
{
    public int CodUsuario { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int StatusConta { get; set; }
    public DateTime CreatedAt { get; set; }
}