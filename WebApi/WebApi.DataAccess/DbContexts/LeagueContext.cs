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

        //public DbSet<League> Leagues { get; set; }

        public DbSet<LeagueEntry> LeagueEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the table names to use singular table names
            //modelBuilder.Entity<League>().ToTable("League");
            modelBuilder.Entity<LeagueEntry>().ToTable("LeagueEntry");
        }
    }
}
