using System.Collections.Generic;
using System.Linq;
using NLog;
using WebApi.DataAccess.DbContexts;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Entities;

namespace WebApi.DataAccess.Repositories
{
    public class SummonerLeagueEntryRepository : ISummonerLeagueEntryRepository
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        private readonly LeagueContext _context;

        public SummonerLeagueEntryRepository(LeagueContext context)
        {
            _context = context;
        }

        public void Add(SummonerLeagueEntry entity)
        {
            if (_context.SummonerLeagueEntries.Any(e => e.PlayerId == entity.PlayerId))
            {
                Update(entity);
                return;
            }

            _context.SummonerLeagueEntries.Add(entity);
            _context.SaveChanges();

            Logger.Debug($"Added {entity}");
        }

        public void Update(SummonerLeagueEntry entity)
        {
            _context.SummonerLeagueEntries.Update(entity);
            _context.SaveChanges();

            Logger.Debug($"Updated {entity}");
        }

        public void Remove(long entityId)
        {
            var entity = _context.SummonerLeagueEntries.First(e => e.PlayerId == entityId);
            _context.SummonerLeagueEntries.Remove(entity);
            _context.SaveChanges();

            Logger.Debug($"Removed {entity}");
        }

        public IEnumerable<SummonerLeagueEntry> GetAll()
        {
            return _context.SummonerLeagueEntries.ToArray();
        }

        public SummonerLeagueEntry Find(long entityId)
        {
            return _context.SummonerLeagueEntries.FirstOrDefault(e => e.PlayerId == entityId);
        }

        public long Count()
        {
            var count = _context.SummonerLeagueEntries.Count();

            Logger.Debug($"{nameof(SummonerLeagueEntry)} Count: {count}");

            return 0;
        }
    }
}
