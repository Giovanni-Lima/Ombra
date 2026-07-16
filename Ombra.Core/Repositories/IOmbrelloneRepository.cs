using Ombra.Models;

namespace Ombra.Repositories;

public interface IOmbrelloneRepository
{
    Task<List<Ombrellone>> GetAllAsync();
    Task<Ombrellone?> GetByIdAsync(int id);
    Task<int> SaveAsync(Ombrellone ombrellone);
    Task DeleteAsync(int id);
}
