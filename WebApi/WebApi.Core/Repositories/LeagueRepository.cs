using System.Collections.Generic;
using System.Linq;
using WebApi.Core.DbContexts;
using WebApi.Core.Repositories.Interfaces;
using WebApi.Model.Dtos.League;

namespace WebApi.Core.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly LeagueContext _context;

        public LeagueRepository(LeagueContext context)
        {
            _context = context;
        }

        public void Add(LeagueDto item)
        {
            _context.Leagues.Add(item);
            _context.SaveChanges();
        }

        public void Remove(string participantId)
        {
            var entity = _context.Leagues.First(t => t.ParticipantId == participantId);
            _context.Leagues.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(LeagueDto item)
        {
            _context.Leagues.Update(item);
            _context.SaveChanges();
        }

        public IEnumerable<LeagueDto> GetAll()
        {
            return _context.Leagues.ToList();
        }

        public LeagueDto Find(string participantId)
        {
            return _context.Leagues.FirstOrDefault(t => t.ParticipantId == participantId);
        }
    }
}
