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

        public void Add(LeagueEntry item)
        {
            if (_context.LeagueEntries.Any(i => i.PlayerOrTeamId == item.PlayerOrTeamId))
            {
                return;
            }

            _context.LeagueEntries.Add(item);
            _context.SaveChanges();
        }

        public void Remove(long playerOrTeamId)
        {
            var item = _context.LeagueEntries.First(t => t.PlayerOrTeamId == playerOrTeamId);
            _context.LeagueEntries.Remove(item);
            _context.SaveChanges();
        }

        public void Update(LeagueEntry item)
        {
            _context.LeagueEntries.Update(item);
            _context.SaveChanges();
        }

        public IEnumerable<LeagueEntry> GetAll()
        {
            return _context.LeagueEntries.ToList();
        }

        public LeagueEntry Find(long playerOrTeamId)
        {
            return _context.LeagueEntries.FirstOrDefault(t => t.PlayerOrTeamId == playerOrTeamId);
        }
    }
}
