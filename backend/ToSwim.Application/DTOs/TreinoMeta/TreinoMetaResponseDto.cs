using System;

namespace ToSwim.Application.DTOs.TreinoMeta;

public class TreinoMetaResponseDto
{
    public int CodTreinoMeta { get; set; }
    public int CodTreino { get; set; }
    public int CodMeta { get; set; }
    public int? CodSerieTreino { get; set; }
    public DateTime CreatedAt { get; set; }

    // Títulos complementares para facilitar a exibição no front-end
    public string TituloMeta { get; set; } = string.Empty;
    public string TituloTreino { get; set; } = string.Empty;
}