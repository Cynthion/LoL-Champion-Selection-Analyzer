using System.Collections.Generic;
using System.Linq;
using WebApi.DataAccess.DbContexts;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Dtos.Match;

namespace WebApi.DataAccess.Repositories
{
    public class MatchReferenceRepository : IMatchReferenceRepository
    {
        private readonly MatchContext _context;

        public MatchReferenceRepository(MatchContext context)
        {
            _context = context;
        }

        public void Add(MatchReference entity)
        {
            if (_context.MatchReferences.Any(i => i.MatchId == entity.MatchId))
            {
                Update(entity);
                return;
            }

            _context.MatchReferences.Add(entity);
            _context.SaveChanges();
        }

        public void Update(MatchReference entity)
        {
            _context.MatchReferences.Update(entity);
            _context.SaveChanges();
        }

        public void Remove(long entityId)
        {
            var item = _context.MatchReferences.First(t => t.MatchId == entityId);
            _context.MatchReferences.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<MatchReference> GetAll()
        {
            return _context.MatchReferences.ToArray();
        }

        public MatchReference Find(long entityId)
        {
            return _context.MatchReferences.FirstOrDefault(t => t.MatchId == entityId);
        }
    }
}
