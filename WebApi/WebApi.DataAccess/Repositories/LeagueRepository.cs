using System.Collections.Generic;
using System.Linq;
using WebApi.DataAccess.DbContexts;
using WebApi.DataAccess.Repositories.Interfaces;
using WebApi.Model.Dtos.League;

namespace WebApi.DataAccess.Repositories
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
            _context.LeagueDtos.Add(item);
            _context.SaveChanges();
        }

        public void Remove(string participantId)
        {
            var entity = _context.LeagueDtos.First(t => t.ParticipantId == participantId);
            _context.LeagueDtos.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(LeagueDto item)
        {
            _context.LeagueDtos.Update(item);
            _context.SaveChanges();
        }

        public IEnumerable<LeagueDto> GetAll()
        {
            return _context.LeagueDtos.ToList();
        }

        public LeagueDto Find(string participantId)
        {
            return _context.LeagueDtos.FirstOrDefault(t => t.ParticipantId == participantId);
        }
    }
}
