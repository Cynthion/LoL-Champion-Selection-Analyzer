using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApi.DataAccess.DbContexts;

namespace WebApi.DataAccess
{
    public class LeagueDbContextFactory : IDbContextFactory<LeagueContext>
    {
        public LeagueContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<LeagueContext>();
            builder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=WebApi;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new LeagueContext(builder.Options);
        }
    }
}
