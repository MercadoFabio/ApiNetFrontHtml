using Parcial.Models;

namespace Parcial.Repositories.Interfaces
{
    public interface IObraRepository
    {
        Task<List<Obra>> GetActiveObras();
        Task AddAlbanilToObra(AlbanilesXObra albanilesXObra);
        Task<List<Albanile>> GetAbanilesNotInObra(Guid obraId);
        Task<bool> GetAlbanilInObra(Guid obraId, Guid albanileId);
    }
}
