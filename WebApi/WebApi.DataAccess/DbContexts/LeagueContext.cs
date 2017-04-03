using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities;

namespace WebApi.DataAccess.DbContexts
{
    public class LeagueContext : DbContext
    {
        public LeagueContext(DbContextOptions<LeagueContext> options)
            : base(options)
        {
        }

        public DbSet<SummonerLeagueEntry> SummonerLeagueEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the table names to use singular table names
            modelBuilder.Entity<SummonerLeagueEntry>().ToTable(nameof(SummonerLeagueEntry));
        }
    }
}
