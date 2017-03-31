using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dtos.League;

namespace WebApi.DataAccess.DbContexts
{
    public class LeagueContext : DbContext
    {
        public LeagueContext(DbContextOptions<LeagueContext> options)
            : base(options)
        {
        }

        public DbSet<LeagueDto> LeagueDtos { get; set; }

        public DbSet<LeagueEntryDto> LeagueEntryDtos { get; set; }
    }
}
