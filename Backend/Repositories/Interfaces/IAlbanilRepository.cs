using Parcial.Models;

namespace Parcial.Repositories.Interfaces
{
    public interface IAlbanilRepository
    {
        Task<List<Albanile>> GetAllActiveAlbaniles();
        Task AddAlbanil(Albanile albanile);
        Task<Albanile> GetAlbanilById(Guid id);
        Task<bool> AlbanilExists(string dni);
    }
}
