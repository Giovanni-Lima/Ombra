using Ombra.Models;
using Ombra.Repositories;

namespace Ombra.Infrastructure;

public class PrenotazioneRepository : IPrenotazioneRepository
{
    private readonly OmbraDatabase _database;

    public PrenotazioneRepository(OmbraDatabase database)
    {
        _database = database;
    }

    public async Task<List<Prenotazione>> GetAllAsync()
    {
        var connection = await _database.GetConnectionAsync();
        return await connection.Table<Prenotazione>().ToListAsync();
    }

    public async Task<List<Prenotazione>> GetByOmbrelloneIdAsync(int ombrelloneId)
    {
        var connection = await _database.GetConnectionAsync();
        return await connection.Table<Prenotazione>().Where(p => p.OmbrelloneId == ombrelloneId).ToListAsync();
    }

    public async Task<Prenotazione?> GetByIdAsync(int id)
    {
        var connection = await _database.GetConnectionAsync();
        return await connection.Table<Prenotazione>().Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(Prenotazione prenotazione)
    {
        var connection = await _database.GetConnectionAsync();
        return prenotazione.Id == 0
            ? await connection.InsertAsync(prenotazione)
            : await connection.UpdateAsync(prenotazione);
    }

    public async Task DeleteAsync(int id)
    {
        var connection = await _database.GetConnectionAsync();
        await connection.DeleteAsync<Prenotazione>(id);
    }
}
