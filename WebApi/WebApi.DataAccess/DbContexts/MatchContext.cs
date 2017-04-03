using Microsoft.EntityFrameworkCore;
using WebApi.Model.Entities;

namespace WebApi.DataAccess.DbContexts
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options)
            : base(options)
        {
        }

        public DbSet<SummonerMatch> SummonerMatches { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the table names to use singular table names
            modelBuilder.Entity<SummonerMatch>().ToTable(nameof(SummonerMatch));
            modelBuilder.Entity<Match>().ToTable(nameof(Match));
            modelBuilder.Entity<Team>().ToTable(nameof(Team));
            modelBuilder.Entity<Participant>().ToTable(nameof(Participant));
        }
    }
}
