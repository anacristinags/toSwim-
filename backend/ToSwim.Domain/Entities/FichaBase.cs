namespace ToSwim.Domain.Entities;

public class FichaBase : IAuditable
{
    public int CodFicha { get; set; }
    public int CodUsuario { get; set; }
    public string TituloFicha { get; set; } = string.Empty;
    public short TipoFicha { get; set; } = 0; // 0 = comum, 1 = meta_tempo
    public short Status { get; set; } = 1; // 0 = inativo, 1 = ativo
    public short TamanhoPiscinaM { get; set; } = 25; // deve ser 25 ou 50
    public int? FichaCopiada { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Propriedades de Navega��o
    public Usuario? Usuario { get; set; }
    public FichaBase? OrigemCopia { get; set; }
    public ICollection<SerieFicha> Series { get; set; } = new List<SerieFicha>();
}