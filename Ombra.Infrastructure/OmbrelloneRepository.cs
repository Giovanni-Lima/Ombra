using Ombra.Models;
using Ombra.Repositories;

namespace Ombra.Infrastructure;

public class OmbrelloneRepository : IOmbrelloneRepository
{
    private readonly OmbraDatabase _database;

    public OmbrelloneRepository(OmbraDatabase database)
    {
        _database = database;
    }

    public async Task<List<Ombrellone>> GetAllAsync()
    {
        var connection = await _database.GetConnectionAsync();
        return await connection.Table<Ombrellone>().ToListAsync();
    }

    public async Task<Ombrellone?> GetByIdAsync(int id)
    {
        var connection = await _database.GetConnectionAsync();
        return await connection.Table<Ombrellone>().Where(o => o.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(Ombrellone ombrellone)
    {
        var connection = await _database.GetConnectionAsync();
        return ombrellone.Id == 0
            ? await connection.InsertAsync(ombrellone)
            : await connection.UpdateAsync(ombrellone);
    }

    public async Task DeleteAsync(int id)
    {
        var connection = await _database.GetConnectionAsync();
        await connection.DeleteAsync<Ombrellone>(id);
    }
}
