using Ombra.Models;

namespace Ombra.Repositories;

public interface IClienteRepository
{
    Task<List<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<int> SaveAsync(Cliente cliente);
    Task DeleteAsync(int id);
}
