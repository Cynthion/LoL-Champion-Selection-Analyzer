using System.Collections.Generic;
using WebApi.Model.Dtos.League;

namespace WebApi.DataAccess.Repositories.Interfaces
{
    public interface ILeagueRepository
    {
        void Add(LeagueDto item);

        void Remove(string participantId);

        void Update(LeagueDto item);

        IEnumerable<LeagueDto> GetAll();

        LeagueDto Find(string participantId);
    }
}
