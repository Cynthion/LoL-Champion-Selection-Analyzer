using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace WebApi.UnitTests
{
    public class DatabaseHelper
    {
        public static DbContextOptions GetInMemoryContext<TContext>([CallerMemberName] string callingFunction = null) where TContext : DbContext
        {
            return new DbContextOptionsBuilder<TContext>()
                .UseInMemoryDatabase(callingFunction ?? "TestDatabaseName")
                .Options;
        }
    }
}
