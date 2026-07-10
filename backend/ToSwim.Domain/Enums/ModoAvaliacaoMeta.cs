namespace ToSwim.Domain.Enums;

public enum ModoAvaliacaoMeta : short
{
    Repeticao = 0, // Avalia cada repetição individualmente
    Media = 1,     // Avalia a média das repetições
    Total = 2      // Avalia o tempo total acumulado da série
}