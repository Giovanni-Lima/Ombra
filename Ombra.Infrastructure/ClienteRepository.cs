using Ombra.Models;
using Ombra.Repositories;

namespace Ombra.Infrastructure;

public class ClienteRepository : IClienteRepository
{
    private readonly OmbraDatabase _database;

    public ClienteRepository(OmbraDatabase database)
    {
        _database = database;
    }

    public async Task<List<Cliente>> GetAllAsync()
    {
        var connection = await _database.GetConnectionAsync();
        return await connection.Table<Cliente>().ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id)
    {
        var connection = await _database.GetConnectionAsync();
        return await connection.Table<Cliente>().Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveAsync(Cliente cliente)
    {
        var connection = await _database.GetConnectionAsync();
        return cliente.Id == 0
            ? await connection.InsertAsync(cliente)
            : await connection.UpdateAsync(cliente);
    }

    public async Task DeleteAsync(int id)
    {
        var connection = await _database.GetConnectionAsync();
        await connection.DeleteAsync<Cliente>(id);
    }
}
