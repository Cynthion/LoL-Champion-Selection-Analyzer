using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.DataAccess.DbContexts;

namespace WebApi.DataAccess
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFramework(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<LeagueContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
