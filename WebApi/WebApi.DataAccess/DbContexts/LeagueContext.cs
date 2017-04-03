using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities.League;

namespace WebApi.DataAccess.DbContexts
{
    public class LeagueContext : DbContext
    {
        public LeagueContext(DbContextOptions<LeagueContext> options)
            : base(options)
        {
        }

        public DbSet<SummonerLeagueEntry> SummonerLeageEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the table names to use singular table names
            modelBuilder.Entity<SummonerLeagueEntry>().ToTable(nameof(SummonerLeagueEntry));
        }
    }
}
