using Microsoft.EntityFrameworkCore;
using WebApi.Model.RiotDtos.Match;
using WebApi.Model.RiotDtos.Matchlist;

namespace WebApi.DataAccess.DbContexts
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options)
            : base(options)
        {
        }

        public DbSet<MatchReferenceDto> MatchReferences { get; set; }

        public DbSet<MatchDetailDto> MatchDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the table names to use singular table names
            modelBuilder.Entity<MatchReferenceDto>().ToTable("MatchReferenceDto");
            modelBuilder.Entity<MatchDetailDto>().ToTable("MatchDetailDto");
        }
    }
}
