using Microsoft.EntityFrameworkCore;
using WebApi.Model.Dtos.Match;

namespace WebApi.DataAccess.DbContexts
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options)
            : base(options)
        {
        }

        public DbSet<MatchReference> MatchReferences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the table names to use singular table names
            modelBuilder.Entity<MatchReference>().ToTable("MatchReference");
        }
    }
}
