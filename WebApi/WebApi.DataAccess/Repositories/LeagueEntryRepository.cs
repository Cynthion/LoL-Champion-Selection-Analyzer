using System.Collections.Generic;
using System.Linq;
using NLog;
using WebApi.DataAccess.DbContexts;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Dtos.League;

namespace WebApi.DataAccess.Repositories
{
    public class LeagueEntryRepository : ILeagueEntryRepository
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly LeagueContext _context;
        
        public LeagueEntryRepository(LeagueContext context)
        {
            _context = context;
        }

        public void Add(LeagueEntry entity)
        {
            if (_context.LeagueEntries.Any(i => i.PlayerOrTeamId == entity.PlayerOrTeamId))
            {
                Update(entity);
                return;
            }

            _context.LeagueEntries.Add(entity);
            _context.SaveChanges();

            Logger.Debug($"Added {entity}");
        }

        public void Update(LeagueEntry entity)
        {
            _context.LeagueEntries.Update(entity);
            _context.SaveChanges();

            Logger.Debug($"Updated {entity}");
        }

        public void Remove(long entityId)
        {
            var entity = _context.LeagueEntries.First(t => t.PlayerOrTeamId == entityId);
            _context.LeagueEntries.Remove(entity);
            _context.SaveChanges();

            Logger.Debug($"Removed {entity}");
        }

        public IEnumerable<LeagueEntry> GetAll()
        {
            return _context.LeagueEntries.ToArray();
        }

        public LeagueEntry Find(long entityId)
        {
            return _context.LeagueEntries.FirstOrDefault(t => t.PlayerOrTeamId == entityId);
        }

        public long Count()
        {
            var count = _context.LeagueEntries.Count();

            Logger.Debug($"Count: {count}");

            return count;
        }
    }
}
