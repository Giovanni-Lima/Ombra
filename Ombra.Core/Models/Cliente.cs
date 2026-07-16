using SQLite;

namespace Ombra.Models;

[Table("Clienti")]
public class Cliente
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Cognome { get; set; } = string.Empty;
    public string? Telefono { get; set; }
}
