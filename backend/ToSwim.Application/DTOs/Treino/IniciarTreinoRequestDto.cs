namespace ToSwim.Application.DTOs.Treino;

public class IniciarTreinoRequestDto
{
    public int CodFicha { get; set; }
    public string TituloTreino { get; set; } = string.Empty;
    public string? Observacao { get; set; }

    // Ignorado pelo backend: o tamanho da piscina do treino e sempre derivado de
    // FichaBase.TamanhoPiscinaM (ja validado contra a configuracao atual do atleta).
    // Mantido apenas por compatibilidade com clientes existentes; o valor enviado aqui
    // nunca e persistido nem usado para autorizar a criacao do treino.
    public short TamanhoPiscinaM { get; set; }
}