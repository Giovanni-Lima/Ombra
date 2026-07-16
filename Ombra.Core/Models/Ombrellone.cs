using SQLite;

namespace Ombra.Models;

[Table("Ombrelloni")]
public class Ombrellone
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int Fila { get; set; }
    public int Numero { get; set; }
    public StatoOmbrellone Stato { get; set; } = StatoOmbrellone.Libero;
}
