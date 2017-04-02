using Microsoft.EntityFrameworkCore;

namespace WebApi.DataAccess.DbContexts
{
    public class LeagueContext : DbContext
    {
        public LeagueContext(DbContextOptions<LeagueContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the table names to use singular table names
            //modelBuilder.Entity<LeagueDto>().ToTable("LeagueDto");
            //modelBuilder.Entity<LeagueEntryDto>().ToTable("LeagueEntryDto");
        }
    }
}
