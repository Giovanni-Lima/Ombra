using Ombra.Models;
using SQLite;

namespace Ombra.Infrastructure;

public class OmbraDatabase
{
    private readonly string _dbPath;
    private SQLiteAsyncConnection? _connection;
    private Task? _initialization;

    public OmbraDatabase(string dbPath)
    {
        _dbPath = dbPath;
    }

    public async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        _initialization ??= InitializeAsync();
        await _initialization;
        return _connection!;
    }

    private async Task InitializeAsync()
    {
        _connection = new SQLiteAsyncConnection(_dbPath);
        await _connection.CreateTableAsync<Ombrellone>();
        await _connection.CreateTableAsync<Cliente>();
        await _connection.CreateTableAsync<Prenotazione>();
        await SeedIfEmptyAsync(_connection);
    }

    private static async Task SeedIfEmptyAsync(SQLiteAsyncConnection connection)
    {
        if (await connection.Table<Ombrellone>().CountAsync() > 0)
            return;

        var ombrelloni = new List<Ombrellone>();
        for (var fila = 1; fila <= 5; fila++)
        {
            for (var numero = 1; numero <= 4; numero++)
            {
                ombrelloni.Add(new Ombrellone { Fila = fila, Numero = numero, Stato = StatoOmbrellone.Libero });
            }
        }
        await connection.InsertAllAsync(ombrelloni);
    }
}
