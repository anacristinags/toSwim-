using System;
using System.Collections.Generic;
using ToSwim.Application.DTOs.SerieTreino;
using ToSwim.Domain.Enums;

namespace ToSwim.Application.DTOs.Treino;

public class TreinoResponseDto
{
    public int CodTreino { get; set; }
    public int CodUsuario { get; set; }
    public int CodFicha { get; set; }
    public string TituloTreino { get; set; } = string.Empty;
    public string? Observacao { get; set; }
    public int DistanciaTotalM { get; set; }
    public decimal DuracaoTotalSeg { get; set; }
    public decimal? PaceMedioSeg { get; set; }
    public short TamanhoPiscinaM { get; set; }
    public DateTime DataTreino { get; set; }
    public StatusTreino Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<SerieTreinoResponseDto> SeriesTreino { get; set; } = new List<SerieTreinoResponseDto>();
}