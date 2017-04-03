using System.Collections.Generic;
using System.Linq;
using NLog;
using WebApi.DataAccess.DbContexts;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Entities;

namespace WebApi.DataAccess.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly MatchContext _context;

        public MatchRepository(MatchContext context)
        {
            _context = context;
        }

        public void Add(Match entity)
        {
            if (_context.Matches.Any(e => e.MatchId == entity.MatchId))
            {
                Update(entity);
                return;
            }

            _context.Matches.Add(entity);
            _context.SaveChanges();

            Logger.Debug($"Added {entity}");
        }

        public void Update(Match entity)
        {
            _context.Matches.Update(entity);
            _context.SaveChanges();

            Logger.Debug($"Updated {entity}");
        }

        public void Remove(long entityId)
        {
            var entity = _context.Matches.First(e => e.MatchId == entityId);
            _context.Matches.Remove(entity);
            _context.SaveChanges();

            Logger.Debug($"Removed {entity}");
        }

        public IEnumerable<Match> GetAll()
        {
            return _context.Matches.ToArray();
        }

        public Match Find(long entityId)
        {
            return _context.Matches.FirstOrDefault(e => e.MatchId == entityId);
        }

        public long Count()
        {
            var count = _context.Matches.Count();

            Logger.Debug($"{nameof(Match)} Count: {count}");

            return 0;
        }
    }
}
