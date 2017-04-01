using System.Collections.Generic;
using System.Linq;
using WebApi.DataAccess.DbContexts;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Dtos.League;

namespace WebApi.DataAccess.Repositories
{
    public class LeagueEntryRepository : ILeagueEntryRepository
    {
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
        }

        public void Update(LeagueEntry entity)
        {
            _context.LeagueEntries.Update(entity);
            _context.SaveChanges();
        }

        public void Remove(long entityId)
        {
            var item = _context.LeagueEntries.First(t => t.PlayerOrTeamId == entityId);
            _context.LeagueEntries.Remove(item);
            _context.SaveChanges();
        }

        public IEnumerable<LeagueEntry> GetAll()
        {
            return _context.LeagueEntries.ToArray();
        }

        public LeagueEntry Find(long entityId)
        {
            return _context.LeagueEntries.FirstOrDefault(t => t.PlayerOrTeamId == entityId);
        }
    }
}
