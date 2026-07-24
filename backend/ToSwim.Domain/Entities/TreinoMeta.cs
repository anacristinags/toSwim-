using System;

namespace ToSwim.Domain.Entities;

public class TreinoMeta
{
    public int CodTreinoMeta { get; set; }
    public int CodTreino { get; set; }
    public int CodMeta { get; set; }
    public int? CodSerieTreino { get; set; } // ON DELETE SET NULL
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Propriedades de Navegação Relacional
    public Treino? Treino { get; set; }
    public Meta? Meta { get; set; }
    public SerieTreino? SerieTreino { get; set; }
}