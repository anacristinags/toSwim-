namespace ToSwim.Application.DTOs.Treino;

public class AtualizarTreinoRequestDto
{
    public string TituloTreino { get; set; } = string.Empty;
    public string? Observacao { get; set; }

    // Ignorado pelo backend: o tamanho da piscina de um treino e fixado na criacao
    // (a partir da FichaBase) e nao pode ser alterado depois. Mantido apenas por
    // compatibilidade; o valor enviado aqui nunca e persistido.
    public short TamanhoPiscinaM { get; set; }
}