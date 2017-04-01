using System.Collections.Generic;
using System.Linq;
using NLog;
using WebApi.DataAccess.DbContexts;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Dtos.Match;

namespace WebApi.DataAccess.Repositories
{
    public class MatchReferenceRepository : IMatchReferenceRepository
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

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

            Logger.Debug($"Added {entity}");
        }

        public void Update(MatchReference entity)
        {
            _context.MatchReferences.Update(entity);
            _context.SaveChanges();

            Logger.Debug($"Updated {entity}");
        }

        public void Remove(long entityId)
        {
            var entity = _context.MatchReferences.First(t => t.MatchId == entityId);
            _context.MatchReferences.Remove(entity);
            _context.SaveChanges();

            Logger.Debug($"Removed {entity}");
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
