namespace ToSwim.Application.DTOs.Treino;

public class IniciarTreinoRequestDto
{
    public int CodFicha { get; set; }
    public string TituloTreino { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    public short TamanhoPiscinaM { get; set; } // CHECK IN (25, 50)
}