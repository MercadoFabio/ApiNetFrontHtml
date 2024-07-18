using Microsoft.EntityFrameworkCore;
using Parcial.Models;
using Parcial.Repositories.Interfaces;

namespace Parcial.Repositories.Impl
{
    public class ObraRepository : IObraRepository
    {
        private readonly DbAa579fProg3w2Context _context;

        public ObraRepository(DbAa579fProg3w2Context context)
        {
            _context = context;
        }

        public async Task AddAlbanilToObra(AlbanilesXObra albanilesXObra)
        {
            await _context.AlbanilesXObras.AddAsync(albanilesXObra);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Albanile>> GetAbanilesNotInObra(Guid obraId)
        {
            var albanilesInObra = _context.AlbanilesXObras
                .Where(x => x.IdObra == obraId).Select(x => x.IdAlbanil);

            var albanilesNotObra = await _context.Albaniles
                .Where(x => !albanilesInObra.Contains(x.Id) && x.Activo)
                .ToListAsync();

            return albanilesNotObra;

        }

        public async Task<List<Obra>> GetActiveObras()
        {
            var obras = await _context.Obras
                .Include(x=>x.IdTipoObraNavigation)
                .Include(x=>x.AlbanilesXObras)
                .ToListAsync();

            return obras;

        }

        public async Task<bool> GetAlbanilInObra(Guid obraId, Guid albanileId)
        {
            var albanilInObra = await _context.AlbanilesXObras
                .AnyAsync(x => x.IdObra == obraId && x.IdAlbanil == albanileId);
           
            return albanilInObra;
        }
    }
}
