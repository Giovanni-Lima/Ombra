using Ombra.Models;

namespace Ombra.Repositories;

public interface IPrenotazioneRepository
{
    Task<List<Prenotazione>> GetAllAsync();
    Task<List<Prenotazione>> GetByOmbrelloneIdAsync(int ombrelloneId);
    Task<Prenotazione?> GetByIdAsync(int id);
    Task<int> SaveAsync(Prenotazione prenotazione);
    Task DeleteAsync(int id);
}
