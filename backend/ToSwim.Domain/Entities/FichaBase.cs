namespace ToSwim.Domain.Entities;

public class FichaBase
{
    public int CodFicha { get; set; }
    public int CodUsuario { get; set; }
    public string TituloFicha { get; set; } = string.Empty;
    public short TipoFicha { get; set; } = 0; // 0 = comum, 1 = meta_tempo
    public short Status { get; set; } = 1; // 0 = inativo, 1 = ativo
    public int? FichaCopiada { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Propriedades de Navegação
    public Usuario? Usuario { get; set; }
    public FichaBase? OrigemCopia { get; set; }
    public ICollection<SerieFicha> Series { get; set; } = new List<SerieFicha>();
}