using System.Collections.Generic;
using System.Linq;
using NLog;
using WebApi.DataAccess.DbContexts;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Entities;

namespace WebApi.DataAccess.Repositories
{
    public class SummonerMatchRepository : ISummonerMatchRepository
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly MatchContext _context;

        public SummonerMatchRepository(MatchContext context)
        {
            _context = context;
        }

        public void Add(SummonerMatch entity)
        {
            if (_context.SummonerMatches.Any(e => e.MatchId == entity.MatchId))
            {
                Update(entity);
                return;
            }

            _context.SummonerMatches.Add(entity);
            _context.SaveChanges();

            Logger.Debug($"Added {entity}");
        }

        public void Update(SummonerMatch entity)
        {
            _context.SummonerMatches.Update(entity);
            _context.SaveChanges();

            Logger.Debug($"Updated {entity}");
        }

        public void Remove(long entityId)
        {
            var entity = _context.SummonerMatches.First(e => e.MatchId == entityId);
            _context.SummonerMatches.Remove(entity);
            _context.SaveChanges();

            Logger.Debug($"Removed {entity}");
        }

        public IEnumerable<SummonerMatch> GetAll()
        {
            return _context.SummonerMatches.ToArray();
        }

        public SummonerMatch Find(long entityId)
        {
            return _context.SummonerMatches.FirstOrDefault(e => e.MatchId == entityId);
        }

        public long Count()
        {
            var count = _context.SummonerMatches.Count();

            Logger.Debug($"{nameof(SummonerMatch)} Count: {count}");

            return 0;
        }
    }
}
