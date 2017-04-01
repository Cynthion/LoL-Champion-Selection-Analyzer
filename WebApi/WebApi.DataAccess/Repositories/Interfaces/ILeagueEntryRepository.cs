using System.Collections.Generic;
using WebApi.Model.Dtos.League;

namespace WebApi.DataAccess.Repositories.Interfaces
{
    public interface ILeagueEntryRepository
    {
        void Add(LeagueEntry item);

        void Remove(long playerOrTeamId);

        void Update(LeagueEntry item);

        IEnumerable<LeagueEntry> GetAll();

        LeagueEntry Find(long playerOrTeamId);
    }
}
