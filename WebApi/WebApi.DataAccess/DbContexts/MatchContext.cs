using Microsoft.EntityFrameworkCore;

namespace WebApi.DataAccess.DbContexts
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the table names to use singular table names
            //modelBuilder.Entity<MatchReferenceDto>().ToTable("MatchReferenceDto");
            //modelBuilder.Entity<MatchDetailDto>().ToTable("MatchDetailDto");
        }
    }
}
