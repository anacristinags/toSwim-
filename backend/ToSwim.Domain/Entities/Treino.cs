using System;
using System.Collections.Generic;
using ToSwim.Domain.Enums;

namespace ToSwim.Domain.Entities;

public class Treino : IAuditable
{
    public int CodTreino { get; set; }
    public int CodUsuario { get; set; }
    public int CodFicha { get; set; }
    public string TituloTreino { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    public int DistanciaTotalM { get; set; } = 0;
    public decimal DuracaoTotalSeg { get; set; } = 0; // int -> decimal
    public decimal? PaceMedioSeg { get; set; }        // int? -> decimal?
    public short TamanhoPiscinaM { get; set; }
    public DateTime DataTreino { get; set; } = DateTime.UtcNow;
    public StatusTreino Status { get; set; } = StatusTreino.Andamento;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Usuario? Usuario { get; set; }
    public FichaBase? FichaBase { get; set; }
    public ICollection<SerieTreino> SeriesTreino { get; set; } = new List<SerieTreino>();
}