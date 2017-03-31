using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dtos.League;

namespace WebApi.Core.DbContexts
{
    public class LeagueContext : DbContext
    {
        public LeagueContext(DbContextOptions<LeagueContext> options)
            : base(options)
        {
        }

        public DbSet<LeagueDto> Leagues { get; set; }

        public DbSet<LeagueEntryDto> LeagueEntrys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the table names to use singular table names
            modelBuilder.Entity<LeagueDto>().ToTable("League");
            modelBuilder.Entity<LeagueEntryDto>().ToTable("LeagueEntry");
        }
    }
}
