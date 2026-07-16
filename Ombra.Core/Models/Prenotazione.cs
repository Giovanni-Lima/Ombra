using SQLite;

namespace Ombra.Models;

[Table("Prenotazioni")]
public class Prenotazione
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int OmbrelloneId { get; set; }
    public int ClienteId { get; set; }
    public DateOnly Data { get; set; }
    public string? Note { get; set; }
}
