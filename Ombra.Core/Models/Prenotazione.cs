using SQLite;

namespace Ombra.Models;

[Table("Prenotazioni")]
public class Prenotazione
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int OmbrelloneId { get; set; }
    public int ClienteId { get; set; }
    // DateTime e non DateOnly: sqlite-net-pcl 1.9.172 non sa mappare System.DateOnly (NotSupportedException a runtime).
    public DateTime Data { get; set; }
    public string? Note { get; set; }
}
