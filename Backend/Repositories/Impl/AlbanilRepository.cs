using Microsoft.EntityFrameworkCore;
using Parcial.Models;
using Parcial.Repositories.Interfaces;

namespace Parcial.Repositories.Impl
{
    public class AlbanilRepository : IAlbanilRepository
    {
        private readonly DbAa579fProg3w2Context _context;

        public AlbanilRepository(DbAa579fProg3w2Context context)
        {
            _context = context;
        }

        public async Task AddAlbanil(Albanile albanile)
        {
            await _context.Albaniles.AddAsync(albanile);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AlbanilExists(string dni)
        {
            var exists = await _context.Albaniles.AnyAsync(x => x.Dni.Equals(dni));
            return exists;
        }

        public async Task<Albanile> GetAlbanilById(Guid id)
        {
            var albanile = await _context.Albaniles.FindAsync(id);
            return albanile;
        }

        public async Task<List<Albanile>> GetAllActiveAlbaniles()
        {
            return await _context.Albaniles.Where(x => x.Activo).ToListAsync();
        }
    }
}
