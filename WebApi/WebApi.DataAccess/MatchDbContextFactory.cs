using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebApi.DataAccess.DbContexts;

namespace WebApi.DataAccess
{
    public class MatchDbContextFactory : IDbContextFactory<MatchContext>
    {
        public MatchContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<MatchContext>();
            builder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=WebApi;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new MatchContext(builder.Options);
        }
    }
}
