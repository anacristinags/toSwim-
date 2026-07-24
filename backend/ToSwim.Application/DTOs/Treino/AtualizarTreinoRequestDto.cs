namespace ToSwim.Application.DTOs.Treino;

public class AtualizarTreinoRequestDto
{
    public string TituloTreino { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    public short TamanhoPiscinaM { get; set; }
}